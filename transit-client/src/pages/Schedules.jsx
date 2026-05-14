import { useState, useEffect } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { routesApi, schedulesApi } from '../api';

const transportIcons = { Bus: 'directions_bus', Trolleybus: 'commute', Tram: 'tram', Minibus: 'airport_shuttle' };
const transportLabels = { Bus: 'Автобус', Trolleybus: 'Троллейбус', Tram: 'Трамвай', Minibus: 'Маршрутка' };
const typeFilters = ['Bus', 'Trolleybus', 'Tram', 'Minibus'];

export default function Schedules() {
  const [routes, setRoutes] = useState([]);
  const [selectedRoute, setSelectedRoute] = useState(null);
  const [schedule, setSchedule] = useState(null);
  const [period, setPeriod] = useState('now');
  const [typeFilter, setTypeFilter] = useState(null);
  const [loading, setLoading] = useState(false);
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  useEffect(() => {
    routesApi.getAll().then(r => {
      setRoutes(r.data);
      
      const paramRouteId = searchParams.get('routeId');
      if (paramRouteId) {
        const found = r.data.find(rt => rt.id === parseInt(paramRouteId));
        if (found) loadSchedule(found);
      }
    });
  }, []);

  const loadSchedule = async (route) => {
    setSelectedRoute(route);
    setLoading(true);
    try {
      const r = await schedulesApi.get(route.id, period);
      setSchedule(r.data);
    } finally { setLoading(false); }
  };

  const changePeriod = async (p) => {
    setPeriod(p);
    if (!selectedRoute) return;
    setLoading(true);
    try {
      const r = await schedulesApi.get(selectedRoute.id, p);
      setSchedule(r.data);
    } finally { setLoading(false); }
  };

  const filteredRoutes = typeFilter ? routes.filter(r => r.type === typeFilter) : routes;

  const periods = [
    { id: 'now', label: 'Сейчас' },
    { id: 'morning', label: 'Утро (06:00 - 12:00)' },
    { id: 'afternoon', label: 'День (12:00 - 18:00)' },
    { id: 'evening', label: 'Вечер (18:00 - 24:00)' },
  ];

  const addToFavorites = (routeId) => {
    const saved = JSON.parse(localStorage.getItem('favRoutes') || '[]');
    if (!saved.includes(routeId)) {
      localStorage.setItem('favRoutes', JSON.stringify([...saved, routeId]));
    }
  };

  return (
    <main className="flex-grow flex flex-col md:flex-row gap-md p-margin-mobile md:p-margin-desktop max-w-[1280px] mx-auto w-full pb-[100px] md:pb-md">
      {}
      <aside className="w-full md:w-1/3 lg:w-1/4 flex flex-col gap-md shrink-0">
        {}
        <div className="bg-surface-container rounded-xl p-sm grid grid-cols-2 gap-base border border-outline-variant/30">
          {[null, ...typeFilters].map((t, i) => (
            <button
              key={i}
              id={t ? `type-${t.toLowerCase()}` : 'type-all'}
              onClick={() => setTypeFilter(t)}
              className={`rounded-lg p-sm flex flex-col items-center justify-center gap-2 transition-colors ${
                typeFilter === t
                  ? 'bg-primary/10 border border-primary/20 hover:bg-primary/20'
                  : 'bg-surface-variant hover:bg-surface-container-high'
              }`}
            >
              <span className={`material-symbols-outlined text-3xl ${typeFilter === t ? 'text-primary' : 'text-on-surface-variant'}`} style={typeFilter === t ? {fontVariationSettings:"'FILL' 1"} : {}}>
                {t ? transportIcons[t] : 'directions_transit'}
              </span>
              <span className={`font-label-sm text-label-sm ${typeFilter === t ? 'text-on-surface' : 'text-on-surface-variant'}`}>
                {t ? transportLabels[t] : 'Все'}
              </span>
            </button>
          ))}
        </div>

        {}
        <div className="bg-surface-container rounded-xl p-md border border-outline-variant/30 flex flex-col gap-sm flex-grow">
          <h3 className="font-headline-md text-headline-md text-on-surface mb-2">Маршруты</h3>
          <div className="flex flex-col gap-2">
            {filteredRoutes.map(route => (
              <button
                key={route.id}
                id={`route-${route.id}`}
                onClick={() => loadSchedule(route)}
                className={`flex items-center justify-between p-sm rounded-lg border transition-colors text-left group ${
                  selectedRoute?.id === route.id
                    ? 'bg-surface-container-high border-primary/30 hover:border-primary/50'
                    : 'bg-surface-container-low border-transparent hover:bg-surface-container-high hover:border-outline-variant/30'
                }`}
              >
                <div className="flex items-center gap-3">
                  <div className={`${selectedRoute?.id === route.id ? 'bg-secondary-container text-on-secondary-container' : 'bg-surface-variant text-on-surface-variant'} w-10 h-10 rounded-full flex items-center justify-center font-headline-md text-headline-md`}>
                    {route.number}
                  </div>
                  <div>
                    <div className="font-label-lg text-label-lg text-on-surface">{route.name}</div>
                    <div className="font-label-sm text-label-sm text-on-surface-variant flex items-center gap-1">
                      <span className="material-symbols-outlined text-[14px]">schedule</span>
                      Каждые {route.frequencyMinutes} мин
                    </div>
                  </div>
                </div>
                <span className={`material-symbols-outlined text-primary transition-opacity ${selectedRoute?.id === route.id ? 'opacity-100' : 'opacity-0 group-hover:opacity-100'}`}>chevron_right</span>
              </button>
            ))}
          </div>
        </div>
      </aside>

      {}
      <section className="flex-grow flex flex-col gap-md">
        {!selectedRoute && (
          <div className="flex-grow flex items-center justify-center">
            <div className="text-center text-on-surface-variant">
              <span className="material-symbols-outlined text-[64px] block mb-4 text-outline">schedule</span>
              <p className="font-headline-md text-headline-md">Выберите маршрут</p>
              <p className="font-body-md text-body-md mt-2">Выберите маршрут из списка слева для просмотра расписания</p>
            </div>
          </div>
        )}

        {selectedRoute && schedule && (
          <>
            {}
            <div className="bg-surface-container-highest/80 backdrop-blur-xl rounded-xl p-md border border-outline-variant/20 relative overflow-hidden">
              <div className="absolute top-0 right-0 w-64 h-64 bg-primary/10 rounded-full blur-3xl -translate-y-1/2 translate-x-1/4"></div>
              <div className="relative z-10 flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
                <div className="flex items-center gap-md">
                  <div className="bg-primary text-on-primary w-16 h-16 rounded-2xl flex items-center justify-center font-headline-xl text-headline-xl shadow-lg shadow-primary/20">
                    {schedule.route.number}
                  </div>
                  <div>
                    <h1 className="font-headline-lg text-headline-lg text-on-surface">{schedule.route.name}</h1>
                    <div className="flex items-center gap-4 mt-1">
                      <span className="bg-secondary/20 text-secondary px-2 py-1 rounded font-label-sm text-label-sm flex items-center gap-1">
                        <span className="material-symbols-outlined text-[14px]">{transportIcons[schedule.route.type]}</span>
                        {transportLabels[schedule.route.type]}
                      </span>
                      <span className="text-on-surface-variant font-body-md text-body-md flex items-center gap-1">
                        <span className="material-symbols-outlined text-[18px]">update</span>
                        Каждые {schedule.route.frequencyMinutes} мин
                      </span>
                    </div>
                  </div>
                </div>
                <div className="flex gap-2 w-full sm:w-auto">
                  <button
                    onClick={() => addToFavorites(selectedRoute.id)}
                    className="bg-surface-variant hover:bg-surface-container-high text-on-surface p-2 rounded-lg transition-colors border border-outline-variant/30 flex items-center justify-center"
                  >
                    <span className="material-symbols-outlined">favorite_border</span>
                  </button>
                </div>
              </div>
            </div>

            {}
            <div className="bg-surface-container rounded-xl border border-outline-variant/30 flex-grow flex flex-col">
              {}
              <div className="border-b border-outline-variant/30 p-sm flex gap-2 overflow-x-auto hide-scrollbar">
                {periods.map(p => (
                  <button
                    key={p.id}
                    id={`period-${p.id}`}
                    onClick={() => changePeriod(p.id)}
                    className={`px-4 py-1.5 rounded-full font-label-lg text-label-lg whitespace-nowrap transition-colors ${
                      period === p.id ? 'bg-primary text-on-primary' : 'bg-surface-variant text-on-surface-variant hover:text-on-surface'
                    }`}
                  >
                    {p.label}
                  </button>
                ))}
              </div>

              {}
              <div className="p-md flex flex-col gap-0 relative">
                <div className="absolute left-[47px] top-md bottom-md w-[2px] bg-outline-variant/30 z-0 hidden sm:block"></div>

                {loading && (
                  <div className="text-center py-8 text-on-surface-variant">Загрузка расписания...</div>
                )}

                {!loading && schedule.departures.length === 0 && (
                  <div className="text-center py-8 text-on-surface-variant font-body-md text-body-md">
                    Нет рейсов в выбранный период
                  </div>
                )}

                {!loading && schedule.stops.map((stopName, stopIdx) => {
                  
                  const nextDep = schedule.departures.find(d => d.isCurrent) || schedule.departures[0];
                  const time = nextDep?.stopTimes?.[stopIdx]?.time;
                  const isFirst = stopIdx === 0;
                  const isLast = stopIdx === schedule.stops.length - 1;
                  const isCurrent = nextDep?.isCurrent && stopIdx === 1;

                  return (
                    <div key={stopIdx} className={`flex flex-col sm:flex-row gap-4 sm:gap-lg py-sm relative z-10 ${isFirst || (stopIdx < 1) ? 'opacity-60' : ''}`}>
                      <div className={`w-full sm:w-24 flex sm:flex-col justify-between sm:justify-start items-center sm:items-end pt-1 ${
                        isCurrent ? 'text-primary font-headline-md text-headline-md' : 'text-on-surface-variant font-body-lg text-body-lg'
                      }`}>
                        <span>{time || '—'}</span>
                        {isCurrent && (
                          <span className="text-error font-label-sm text-label-sm sm:mt-1 flex items-center gap-1">
                            <span className="material-symbols-outlined text-[14px]">radio_button_checked</span> Следующая
                          </span>
                        )}
                      </div>

                      <div className={`hidden sm:flex items-center justify-center w-6 h-6 rounded-full border-2 mt-1 ${
                        isFirst ? 'bg-surface border-outline-variant' :
                        isLast ? 'bg-surface border-outline-variant' :
                        isCurrent ? 'bg-surface border-primary relative' :
                        'bg-surface border-outline-variant'
                      }`}>
                        {isCurrent && <div className="w-2 h-2 rounded-full bg-primary animate-pulse"></div>}
                        {isLast && <span className="material-symbols-outlined text-[16px] text-outline-variant">flag</span>}
                      </div>

                      <div className={`flex-grow rounded-lg p-sm border ${
                        isCurrent
                          ? 'bg-secondary-container/20 border-primary/30 shadow-[0_0_15px_rgba(255,181,153,0.1)]'
                          : isFirst ? 'bg-surface-container-high border-outline-variant/20'
                          : 'bg-surface-container-low border-outline-variant/10'
                      }`}>
                        <h4 className="font-headline-md text-headline-md text-on-surface">{stopName}</h4>
                        {isFirst && <div className="text-on-surface-variant font-body-md text-body-md mt-1">Начальная остановка</div>}
                        {isLast && <div className="text-tertiary font-body-md text-body-md mt-1">Конечная остановка</div>}
                      </div>
                    </div>
                  );
                })}
              </div>

              {}
              {!loading && schedule.departures.length > 0 && (
                <div className="border-t border-outline-variant/30 p-md">
                  <h3 className="font-label-lg text-label-lg text-on-surface-variant mb-sm">Все отправления</h3>
                  <div className="flex flex-wrap gap-xs">
                    {schedule.departures.slice(0, 40).map((dep, i) => (
                      <span key={i} className={`font-label-sm text-label-sm px-2 py-1 rounded ${
                        dep.isCurrent ? 'bg-primary text-on-primary' : 'bg-surface-variant text-on-surface-variant'
                      }`}>
                        {dep.departure}
                      </span>
                    ))}
                  </div>
                </div>
              )}
            </div>
          </>
        )}
      </section>
    </main>
  );
}
