import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { routesApi } from '../api';
import { useToast } from '../components/ToastContext';

const transportIcons = { Bus: 'directions_bus', Trolleybus: 'commute', Tram: 'tram', Minibus: 'airport_shuttle' };
const transportColors = {
  Bus: 'bg-secondary-container text-on-secondary-container',
  Trolleybus: 'bg-tertiary-container text-on-tertiary-container',
  Tram: 'bg-secondary text-on-secondary',
  Minibus: 'bg-surface-variant text-on-surface'
};

export default function Favorites() {
  const [favRoutes, setFavRoutes] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const { showToast } = useToast();

  useEffect(() => {
    const savedRoutes = JSON.parse(localStorage.getItem('favRoutes') || '[]');
    if (savedRoutes.length === 0) {
      setLoading(false);
      return;
    }

    Promise.all(savedRoutes.map(id => routesApi.getById(id).catch(() => null)))
      .then(results => {
        setFavRoutes(results.filter(Boolean).map(r => r.data));
      })
      .finally(() => setLoading(false));
  }, []);

  const removeFavRoute = (id) => {
    const saved = JSON.parse(localStorage.getItem('favRoutes') || '[]').filter(i => i !== id);
    localStorage.setItem('favRoutes', JSON.stringify(saved));
    setFavRoutes(prev => prev.filter(r => r.id !== id));
    showToast('Маршрут удален из избранного', 'info');
  };

  return (
    <main className="flex-grow w-full max-w-[1280px] mx-auto px-4 md:px-8 py-4 md:py-6 flex flex-col gap-5 pb-[80px] md:pb-6">
      {/* Header */}
      <header className="flex flex-col gap-3">
        <h1 className="text-2xl md:text-3xl font-bold text-on-background">Избранные маршруты</h1>
      </header>

      {loading ? (
        <div className="text-center text-on-surface-variant py-12 flex flex-col items-center gap-3">
          <span className="material-symbols-outlined animate-spin text-4xl text-outline">progress_activity</span>
          <span className="text-sm">Загрузка...</span>
        </div>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
          {favRoutes.length === 0 ? (
            <div className="col-span-full bg-surface-container rounded-2xl p-8 text-on-surface-variant text-sm text-center border border-outline-variant/30">
              <span className="material-symbols-outlined text-5xl block mb-3 text-outline">route</span>
              Нет избранных маршрутов. Добавьте их на странице расписаний.
            </div>
          ) : (
            favRoutes.map(route => (
              <div
                key={route.id}
                className="card-hover bg-surface-container-low border border-outline-variant rounded-2xl p-4 flex flex-col gap-2 cursor-pointer"
                onClick={() => navigate(`/schedules?routeId=${route.id}`)}
              >
                <div className="flex justify-between items-center">
                  <div className="flex items-center gap-3">
                    <span className={`${transportColors[route.type] || 'bg-primary-container text-on-primary-container'} font-bold text-base px-3 py-1.5 rounded-xl flex items-center gap-1.5`}>
                      <span className="material-symbols-outlined text-[16px]">{transportIcons[route.type]}</span>
                      {route.number}
                    </span>
                  </div>
                  <button
                    className="btn-icon text-primary hover:text-primary-fixed"
                    onClick={e => { e.stopPropagation(); removeFavRoute(route.id); }}
                  >
                    <span className="material-symbols-outlined" style={{fontVariationSettings:"'FILL' 1"}}>favorite</span>
                  </button>
                </div>
                <p className="text-sm text-on-surface font-semibold leading-tight">{route.name}</p>
                <div className="flex items-center gap-1.5">
                  <span className="material-symbols-outlined text-outline text-[15px]">schedule</span>
                  <span className="text-xs text-outline">Каждые 15 мин</span>
                </div>
              </div>
            ))
          )}
        </div>
      )}
    </main>
  );
}
