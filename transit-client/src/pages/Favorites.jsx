import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { stopsApi, routesApi } from '../api';

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

  
  useEffect(() => {
    const savedStops = JSON.parse(localStorage.getItem('favStops') || '[]');
    const savedRoutes = JSON.parse(localStorage.getItem('favRoutes') || '[]');

    
    const defaultStopIds = savedStops.length ? savedStops : [2, 4];
    const defaultRouteIds = savedRoutes.length ? savedRoutes : [1, 2];

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
  };
  const removeFavRoute = (id) => {
    const saved = JSON.parse(localStorage.getItem('favRoutes') || '[]').filter(i => i !== id);
    localStorage.setItem('favRoutes', JSON.stringify(saved));
    setFavRoutes(prev => prev.filter(r => r.id !== id));
  };

  const filters = [
    { id: 'all', label: 'Все' },
    { id: 'stops', label: 'Остановки' },
    { id: 'routes', label: 'Маршруты' },
  ];

  return (
    <main className="flex-grow w-full max-w-[1280px] mx-auto px-margin-mobile md:px-margin-desktop py-lg flex flex-col gap-lg pb-[100px] md:pb-lg">
      {}
      <header className="flex flex-col gap-md">
        <h1 className="font-headline-xl text-headline-xl text-on-background">Избранное</h1>
        <div className="flex flex-wrap gap-sm">
          {filters.map(f => (
            <button
              key={f.id}
              id={`filter-${f.id}`}
              onClick={() => setActiveFilter(f.id)}
              className={`font-label-lg text-label-lg px-gutter py-2 rounded-full transition-colors ${
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

      {}
      <div className="grid grid-cols-1 md:grid-cols-12 gap-md auto-rows-[auto]">
        {}
        {(activeFilter === 'all' || activeFilter === 'stops') && (
          <div className="col-span-1 md:col-span-8 flex flex-col gap-sm">
            <h2 className="font-headline-md text-headline-md text-on-surface-variant mb-xs">Любимые остановки</h2>
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-sm">
              {favStops.length === 0 && (
                <div className="col-span-2 bg-surface-container rounded-xl p-md text-on-surface-variant font-body-md text-body-md">
                  Нет избранных остановок. Добавьте их на странице расписаний.
                </div>
              )}
              {favStops.map(stop => (
                <div
                  key={stop.id}
                  className="bg-surface-container rounded-xl p-md flex flex-col gap-md hover:bg-surface-container-high transition-colors border border-transparent hover:border-outline-variant relative overflow-hidden group cursor-pointer"
                  onClick={() => navigate(`/stops/${stop.id}`)}
                >
                  <div className="flex justify-between items-start">
                    <div className="flex items-center gap-sm">
                      <div className="bg-secondary-container text-on-secondary-container p-2 rounded-lg flex items-center justify-center">
                        <span className="material-symbols-outlined">directions_bus</span>
                      </div>
                      <div className="flex flex-col">
                        <h3 className="font-headline-md text-headline-md text-on-background line-clamp-1">{stop.name}</h3>
                        <span className="font-body-md text-body-md text-on-surface-variant">{stop.address}</span>
                      </div>
                    </div>
                    <button
                      className="text-primary hover:text-primary-fixed transition-colors"
                      onClick={e => { e.stopPropagation(); removeFavStop(stop.id); }}
                    >
                      <span className="material-symbols-outlined" style={{fontVariationSettings:"'FILL' 1"}}>favorite</span>
                    </button>
                  </div>
                  <div className="flex flex-col gap-xs mt-auto">
                    {(stopArrivals[stop.id] || []).map((arr, i) => (
                      <div key={i} className="flex items-center justify-between bg-surface-dim rounded-lg p-sm">
                        <div className="flex items-center gap-sm">
                          <span className={`${routeTagColors[arr.type] || 'bg-surface-variant text-on-surface'} font-label-lg text-label-lg px-2 py-1 rounded-md`}>
                            {arr.routeNumber}
                          </span>
                          <span className="font-body-md text-body-md text-on-surface line-clamp-1">{arr.routeName}</span>
                        </div>
                        <span className={`font-headline-md text-headline-md ${arr.minutesUntil <= 2 ? 'text-primary' : 'text-tertiary-fixed'}`}>
                          {arr.minutesUntil <= 0 ? 'Прибывает' : `${arr.minutesUntil} мин`}
                        </span>
                      </div>
                    ))}
                    {(!stopArrivals[stop.id] || stopArrivals[stop.id].length === 0) && (
                      <div className="bg-surface-dim rounded-lg p-sm text-on-surface-variant font-label-sm text-label-sm text-center">
                        Нет ближайших рейсов
                      </div>
                    )}
                  </div>
                </div>
              ))}
            </div>
          </div>
        )}

        {}
        {(activeFilter === 'all' || activeFilter === 'routes') && (
          <div className="col-span-1 md:col-span-4 flex flex-col gap-md">
            <div className="flex flex-col gap-sm">
              <h2 className="font-headline-md text-headline-md text-on-surface-variant mb-xs">Маршруты</h2>
              {favRoutes.length === 0 && (
                <div className="bg-surface-container-low border border-outline-variant rounded-xl p-md text-on-surface-variant font-body-md text-body-md">
                  Нет избранных маршрутов.
                </div>
              )}
              {favRoutes.map(route => (
                <div
                  key={route.id}
                  className="bg-surface-container-low border border-outline-variant rounded-xl p-md flex flex-col gap-sm hover:bg-surface-container transition-colors cursor-pointer"
                  onClick={() => navigate(`/schedules?routeId=${route.id}`)}
                >
                  <div className="flex justify-between items-center">
                    <div className="flex items-center gap-sm">
                      <span className={`${transportColors[route.type] || 'bg-primary-container text-on-primary-container'} font-headline-md text-headline-md px-3 py-1 rounded-lg`}>
                        {route.number}
                      </span>
                    </div>
                    <button
                      className="text-primary hover:text-primary-fixed transition-colors"
                      onClick={e => { e.stopPropagation(); removeFavRoute(route.id); }}
                    >
                      <span className="material-symbols-outlined" style={{fontVariationSettings:"'FILL' 1"}}>favorite</span>
                    </button>
                  </div>
                  <p className="font-body-lg text-body-lg text-on-surface leading-tight mt-xs">{route.name}</p>
                  <div className="flex items-center gap-xs mt-xs">
                    <span className="material-symbols-outlined text-outline text-[18px]">schedule</span>
                    <span className="font-label-sm text-label-sm text-outline">Ходит каждые {route.frequencyMinutes} мин</span>
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
