import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { stopsApi, routesApi } from '../api';
import { useToast } from '../components/ToastContext';

const transportIcons = { Bus: 'directions_bus', Trolleybus: 'commute', Tram: 'tram', Minibus: 'airport_shuttle' };
const transportColors = {
  Bus: 'bg-secondary-container text-on-secondary-container',
  Trolleybus: 'bg-tertiary-container text-on-tertiary-container',
  Tram: 'bg-secondary text-on-secondary',
  Minibus: 'bg-surface-variant text-on-surface'
};
const routeTagColors = {
  Bus: 'bg-primary text-on-primary',
  Trolleybus: 'bg-tertiary text-on-tertiary',
  Tram: 'bg-secondary text-on-secondary',
  Minibus: 'bg-surface-variant text-on-surface'
};

export default function Favorites() {
  const [activeFilter, setActiveFilter] = useState('all');
  const [favStops, setFavStops] = useState([]);
  const [favRoutes, setFavRoutes] = useState([]);
  const [stopArrivals, setStopArrivals] = useState({});
  const navigate = useNavigate();
  const { showToast } = useToast();

  useEffect(() => {
    const savedStops  = JSON.parse(localStorage.getItem('favStops')  || '[]');
    const savedRoutes = JSON.parse(localStorage.getItem('favRoutes') || '[]');

    const defaultStopIds  = savedStops;
    const defaultRouteIds = savedRoutes;

    Promise.all(defaultStopIds.map(id => stopsApi.getById(id).catch(() => null)))
      .then(results => setFavStops(results.filter(Boolean).map(r => r.data)));

    Promise.all(defaultRouteIds.map(id => routesApi.getById(id).catch(() => null)))
      .then(results => setFavRoutes(results.filter(Boolean).map(r => r.data)));
  }, []);

  useEffect(() => {
    favStops.forEach(stop => {
      stopsApi.getArrivals(stop.id).then(r => {
        setStopArrivals(prev => ({ ...prev, [stop.id]: r.data.slice(0, 2) }));
      }).catch(() => {});
    });
  }, [favStops]);

  const removeFavStop = (id) => {
    const saved = JSON.parse(localStorage.getItem('favStops') || '[]').filter(i => i !== id);
    localStorage.setItem('favStops', JSON.stringify(saved));
    setFavStops(prev => prev.filter(s => s.id !== id));
    showToast('Остановка удалена из избранного', 'info');
  };
  const removeFavRoute = (id) => {
    const saved = JSON.parse(localStorage.getItem('favRoutes') || '[]').filter(i => i !== id);
    localStorage.setItem('favRoutes', JSON.stringify(saved));
    setFavRoutes(prev => prev.filter(r => r.id !== id));
    showToast('Маршрут удален из избранного', 'info');
  };

  const filters = [
    { id: 'all',    label: 'Все' },
    { id: 'stops',  label: 'Остановки' },
    { id: 'routes', label: 'Маршруты' },
  ];

  return (
    <main className="flex-grow w-full max-w-[1280px] mx-auto px-4 md:px-8 py-4 md:py-6 flex flex-col gap-5 pb-[80px] md:pb-6">
      {/* Header */}
      <header className="flex flex-col gap-3">
        <h1 className="text-2xl md:text-3xl font-bold text-on-background">Избранное</h1>
        <div className="flex flex-wrap gap-2">
          {filters.map(f => (
            <button
              key={f.id}
              id={`filter-${f.id}`}
              onClick={() => setActiveFilter(f.id)}
              className={`btn-chip text-sm font-semibold px-4 py-2 rounded-full ${
                activeFilter === f.id
                  ? 'bg-primary text-on-primary'
                  : 'bg-surface-variant text-on-surface-variant hover:bg-surface-container-highest'
              }`}
            >
              {f.label}
            </button>
          ))}
        </div>
      </header>

      <div className="grid grid-cols-1 md:grid-cols-12 gap-4">
        {/* Stops */}
        {(activeFilter === 'all' || activeFilter === 'stops') && (
          <div className="col-span-1 md:col-span-8 flex flex-col gap-3">
            <h2 className="text-base font-bold text-on-surface-variant">Любимые остановки</h2>
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
              {favStops.length === 0 && (
                <div className="col-span-2 bg-surface-container rounded-2xl p-5 text-on-surface-variant text-sm">
                  Нет избранных остановок. Добавьте их на странице расписаний.
                </div>
              )}
              {favStops.map(stop => (
                <div
                  key={stop.id}
                  className="card-hover bg-surface-container rounded-2xl p-4 flex flex-col gap-3 border border-transparent hover:border-outline-variant relative overflow-hidden cursor-pointer"
                  onClick={() => navigate(`/stops/${stop.id}`)}
                >
                  <div className="flex justify-between items-start">
                    <div className="flex items-center gap-3">
                      <div className="bg-secondary-container text-on-secondary-container p-2 rounded-xl flex items-center justify-center">
                        <span className="material-symbols-outlined text-[22px]">directions_bus</span>
                      </div>
                      <div className="flex flex-col min-w-0">
                        <h3 className="font-bold text-base text-on-background truncate">{stop.name}</h3>

                      </div>
                    </div>
                    <button
                      className="btn-icon text-primary hover:text-primary-fixed flex-shrink-0"
                      onClick={e => { e.stopPropagation(); removeFavStop(stop.id); }}
                    >
                      <span className="material-symbols-outlined" style={{fontVariationSettings:"'FILL' 1"}}>favorite</span>
                    </button>
                  </div>
                  <div className="flex flex-col gap-2 mt-auto">
                    {(stopArrivals[stop.id] || []).map((arr, i) => (
                      <div key={i} className="flex items-center justify-between bg-surface-dim rounded-xl p-2.5">
                        <div className="flex items-center gap-2 min-w-0">
                          <span className={`${routeTagColors[arr.type] || 'bg-surface-variant text-on-surface'} text-xs font-bold px-2 py-1 rounded-lg flex-shrink-0`}>
                            {arr.routeNumber}
                          </span>
                          <span className="text-sm text-on-surface truncate">{arr.routeName}</span>
                        </div>
                        <span className={`text-base font-bold flex-shrink-0 ml-2 ${arr.minutesUntil <= 2 ? 'text-primary' : 'text-tertiary-fixed'}`}>
                          {arr.minutesUntil <= 0 ? 'Прибывает' : `${arr.minutesUntil} мин`}
                        </span>
                      </div>
                    ))}
                    {(!stopArrivals[stop.id] || stopArrivals[stop.id].length === 0) && (
                      <div className="bg-surface-dim rounded-xl p-2.5 text-on-surface-variant text-xs text-center">
                        Нет ближайших рейсов
                      </div>
                    )}
                  </div>
                </div>
              ))}
            </div>
          </div>
        )}

        {/* Routes */}
        {(activeFilter === 'all' || activeFilter === 'routes') && (
          <div className="col-span-1 md:col-span-4 flex flex-col gap-3">
            <h2 className="text-base font-bold text-on-surface-variant">Маршруты</h2>
            {favRoutes.length === 0 && (
              <div className="bg-surface-container-low border border-outline-variant rounded-2xl p-5 text-on-surface-variant text-sm">
                Нет избранных маршрутов.
              </div>
            )}
            <div className="flex flex-col gap-2.5">
              {favRoutes.map(route => (
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
              ))}
            </div>
          </div>
        )}
      </div>
    </main>
  );
}
