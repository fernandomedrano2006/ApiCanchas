const tabs = document.querySelectorAll('.tab');
const content = document.getElementById('content');
const sectionTitle = document.getElementById('sectionTitle');
const refreshBtn = document.getElementById('refreshBtn');
const countHorarios = document.getElementById('countHorarios');
const countServicios = document.getElementById('countServicios');
const countPromos = document.getElementById('countPromos');

const endpointLabels = {
  '/api/horarios': 'Horarios disponibles',
  '/api/servicios': 'Servicios premium',
  '/api/promociones': 'Promociones activas'
};

const cardTemplate = document.getElementById('cardTemplate');

async function loadData(endpoint) {
  sectionTitle.textContent = endpointLabels[endpoint] || 'Contenido';
  content.innerHTML = '<div class="empty">Cargando...</div>';

  try {
    const response = await fetch(endpoint, { headers: { Accept: 'application/json' } });
    if (!response.ok) {
      throw new Error(`HTTP ${response.status}`);
    }

    const data = await response.json();
    renderCards(data, endpoint);
  } catch (error) {
    content.innerHTML = `<div class="empty">No se pudo cargar la información. ${error.message}</div>`;
  }
}

function getValue(item, keys) {
  for (const key of keys) {
    if (item && Object.prototype.hasOwnProperty.call(item, key)) {
      return item[key];
    }
  }

  return '';
}

function renderCards(data, endpoint) {
  if (!Array.isArray(data) || data.length === 0) {
    content.innerHTML = '<div class="empty">No hay información disponible en este momento.</div>';
    return;
  }

  content.innerHTML = '';

  data.forEach((item) => {
    const clone = cardTemplate.content.cloneNode(true);
    const title = clone.querySelector('.title');
    const badge = clone.querySelector('.badge');
    const description = clone.querySelector('.description');
    const meta = clone.querySelector('.meta');

    if (endpoint === '/api/horarios') {
      const fecha = getValue(item, ['Fecha', 'fecha']);
      const hora = getValue(item, ['Hora', 'hora']);
      const estado = getValue(item, ['Estado', 'estado']);
      const tipoCancha = getValue(item, ['TipoCancha', 'tipoCancha']);
      const duracion = getValue(item, ['Duracion', 'duracion']);
      const precio = Number(getValue(item, ['Precio', 'precio']) ?? 0);

      title.textContent = `${fecha} · ${hora}`;
      badge.textContent = estado;
      badge.className = 'badge ' + (estado === 'Disponible' ? '' : estado === 'Ocupado' ? 'warning' : 'muted');
      description.textContent = `${tipoCancha} · ${duracion}`;
      meta.innerHTML = `
        <span>Precio: $${precio.toFixed(2)}</span>
        <span>Cancha: ${tipoCancha}</span>
      `;
    }

    if (endpoint === '/api/servicios') {
      title.textContent = getValue(item, ['Nombre', 'nombre']);
      badge.textContent = 'Servicio';
      badge.className = 'badge';
      description.textContent = getValue(item, ['Descripcion', 'descripcion']);
      meta.innerHTML = `<span>ID: ${getValue(item, ['ID', 'id'])}</span>`;
    }

    if (endpoint === '/api/promociones') {
      title.textContent = getValue(item, ['Titulo', 'titulo']);
      badge.textContent = 'Oferta';
      badge.className = 'badge';
      description.textContent = getValue(item, ['Descripcion', 'descripcion']);
      meta.innerHTML = `<span>ID: ${getValue(item, ['ID', 'id'])}</span>`;
    }

    content.appendChild(clone);
  });

  updateCounts(data, endpoint);
}

function updateCounts(data, endpoint) {
  if (endpoint === '/api/horarios') {
    countHorarios.textContent = data.length;
  }

  if (endpoint === '/api/servicios') {
    countServicios.textContent = data.length;
  }

  if (endpoint === '/api/promociones') {
    countPromos.textContent = data.length;
  }
}

tabs.forEach((tab) => {
  tab.addEventListener('click', () => {
    tabs.forEach((button) => button.classList.toggle('active', button === tab));
    loadData(tab.dataset.endpoint);
  });
});

refreshBtn.addEventListener('click', () => {
  const activeTab = document.querySelector('.tab.active');
  if (activeTab) {
    loadData(activeTab.dataset.endpoint);
  }
});

loadData('/api/horarios');
