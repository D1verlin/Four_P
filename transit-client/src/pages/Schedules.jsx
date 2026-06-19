import { useState, useEffect } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { routesApi, schedulesApi } from '../api';
import { useToast } from '../components/ToastContext';

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
  const { showToast } = useToast();

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
    { id: 'now',       label: 'Сейчас' },
    { id: 'morning',   label: 'Утро 06–12' },
    { id: 'afternoon', label: 'День 12–18' },
    { id: 'evening',   label: 'Вечер 18–24' },
  ];

  const addToFavorites = (routeId) => {
    const saved = JSON.parse(localStorage.getItem('favRoutes') || '[]');
    if (!saved.includes(routeId)) {
      localStorage.setItem('favRoutes', JSON.stringify([...saved, routeId]));
      showToast('Маршрут добавлен в избранное!', 'success');
    } else {
      showToast('Маршрут уже в избранном!', 'info');
    }
  };

  return (
    <main className="flex-grow flex flex-col md:flex-row gap-4 px-4 md:px-8 py-4 md:py-6 max-w-[1280px] mx-auto w-full pb-[80px] md:pb-6">
      {/* === SIDEBAR === */}
      <aside className="w-full md:w-72 lg:w-64 flex flex-col gap-3 shrink-0">
        {/* Transport type filter grid */}
        <div className="bg-surface-container rounded-2xl p-3 grid grid-cols-5 md:grid-cols-3 gap-2 border border-outline-variant/30">
          {[null, ...typeFilters].map((t, i) => (
            <button
              key={i}
              id={t ? `type-${t.toLowerCase()}` : 'type-all'}
              onClick={() => setTypeFilter(t)}
              className={`btn-chip rounded-xl p-2 flex flex-col items-center justify-center gap-1 ${
                typeFilter === t
                  ? 'bg-primary/15 border border-primary/30'
                  : 'bg-surface-variant hover:bg-surface-container-high'
              }`}
            >
              <span className={`material-symbols-outlined text-2xl ${typeFilter === t ? 'text-primary' : 'text-on-surface-variant'}`}
                style={typeFilter === t ? {fontVariationSettings:"'FILL' 1"} : {}}>
                {t ? transportIcons[t] : 'directions_transit'}
              </span>
              <span className={`text-[10px] font-semibold leading-tight text-center ${typeFilter === t ? 'text-on-surface' : 'text-on-surface-variant'}`}>
                {t ? transportLabels[t] : 'Все'}
              </span>
            </button>
          ))}
        </div>

        {/* Routes list */}
        <div className="bg-surface-container rounded-2xl p-3 border border-outline-variant/30 flex flex-col gap-2 md:flex-grow">
          <h3 className="text-base font-bold text-on-surface px-1">Маршруты</h3>
          <div className="flex flex-col gap-1.5 max-h-60 md:max-h-none overflow-y-auto custom-scrollbar">
            {filteredRoutes.map(route => (
              <button
                key={route.id}
                id={`route-${route.id}`}
                onClick={() => loadSchedule(route)}
                className={`flex items-center justify-between p-2.5 rounded-xl border transition-all text-left group ${
                  selectedRoute?.id === route.id
                    ? 'bg-surface-container-high border-primary/30'
                    : 'bg-surface-container-low border-transparent hover:bg-surface-container-high hover:border-outline-variant/30'
                }`}
              >
                <div className="flex items-center gap-2.5">
                  <div className={`${selectedRoute?.id === route.id ? 'bg-primary text-on-primary' : 'bg-surface-variant text-on-surface-variant'} w-9 h-9 rounded-full flex items-center justify-center font-bold text-sm transition-colors`}>
                    {route.number}
                  </div>
                  <div>
                    <div className="font-semibold text-sm text-on-surface leading-tight">{route.name}</div>
                    <div className="text-xs text-on-surface-variant flex items-center gap-1 mt-0.5">
                      <span className="material-symbols-outlined text-[12px]">schedule</span>
                      каждые 15 мин
                    </div>
                  </div>
                </div>
                <span className={`material-symbols-outlined text-primary text-[18px] transition-opacity ${selectedRoute?.id === route.id ? 'opacity-100' : 'opacity-0 group-hover:opacity-60'}`}>chevron_right</span>
              </button>
            ))}
          </div>
        </div>
      </aside>

      {/* === MAIN CONTENT === */}
      <section className="flex-grow flex flex-col gap-4 min-w-0">
        {!selectedRoute && (
          <div className="flex-grow flex items-center justify-center py-20">
            <div className="text-center text-on-surface-variant animate-fade-in">
              <span className="material-symbols-outlined text-[64px] block mb-4 text-outline">schedule</span>
              <p className="text-xl font-bold">Выберите маршрут</p>
              <p className="text-sm mt-2 text-on-surface-variant/70">Выберите маршрут из списка слева</p>
            </div>
          </div>
        )}

        {selectedRoute && schedule && (
          <>
            {/* Route header card */}
            <div className="bg-surface-container-highest/80 backdrop-blur-xl rounded-2xl p-4 md:p-5 border border-outline-variant/20 relative overflow-hidden animate-slide-up">
              <div className="absolute top-0 right-0 w-48 h-48 bg-primary/10 rounded-full blur-3xl -translate-y-1/2 translate-x-1/4 pointer-events-none"></div>
              <div className="relative z-10 flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
                <div className="flex items-center gap-4">
                  <div className="bg-primary text-on-primary w-14 h-14 rounded-2xl flex items-center justify-center font-bold text-xl shadow-lg shadow-primary/20 flex-shrink-0">
                    {schedule.route.number}
                  </div>
                  <div>
                    <h1 className="text-lg md:text-xl font-bold text-on-surface leading-tight">{schedule.route.name}</h1>
                    <div className="flex flex-wrap items-center gap-3 mt-1">
                      <span className="bg-secondary/20 text-secondary px-2 py-1 rounded-lg text-xs font-semibold flex items-center gap-1">
                        <span className="material-symbols-outlined text-[13px]">{transportIcons[schedule.route.type]}</span>
                        {transportLabels[schedule.route.type]}
                      </span>
                      <span className="text-on-surface-variant text-xs flex items-center gap-1">
                        <span className="material-symbols-outlined text-[15px]">update</span>
                        каждые 15 мин
                      </span>
                    </div>
                  </div>
                </div>
                <button
                  onClick={() => addToFavorites(selectedRoute.id)}
                  className="btn-icon bg-surface-variant hover:bg-surface-container-high text-on-surface p-2.5 rounded-xl border border-outline-variant/30 flex items-center justify-center"
                >
                  <span className="material-symbols-outlined">favorite_border</span>
                </button>
              </div>
            </div>

            {/* Schedule card */}
            <div className="bg-surface-container rounded-2xl border border-outline-variant/30 flex-grow flex flex-col overflow-hidden">
              {/* Period filter tabs */}
              <div className="border-b border-outline-variant/30 p-3 flex gap-2 overflow-x-auto hide-scrollbar">
                {periods.map(p => (
                  <button
                    key={p.id}
                    id={`period-${p.id}`}
                    onClick={() => changePeriod(p.id)}
                    className={`btn-chip px-4 py-2 rounded-full text-sm font-semibold whitespace-nowrap ${
                      period === p.id ? 'bg-primary text-on-primary' : 'bg-surface-variant text-on-surface-variant hover:text-on-surface'
                    }`}
                  >
                    {p.label}
                  </button>
                ))}
              </div>

              {/* Stops timeline */}
              <div className="p-4 flex flex-col gap-0 relative overflow-y-auto custom-scrollbar">
                <div className="absolute left-[63px] top-4 bottom-4 w-[2px] bg-outline-variant/30 z-0 hidden sm:block"></div>

                {loading && (
                  <div className="text-center py-10 text-on-surface-variant flex items-center justify-center gap-2">
                    <span className="material-symbols-outlined animate-spin">progress_activity</span>
                    Загрузка расписания...
                  </div>
                )}

                {!loading && schedule.departures.length === 0 && (
                  <div className="text-center py-10 text-on-surface-variant text-sm">
                    Нет рейсов в выбранный период
                  </div>
                )}

                {!loading && (() => {
                  const nextDep = schedule.departures.find(d => d.isCurrent) || schedule.departures[0];
                  let nextStopIdx = -1;
                  if (nextDep && nextDep.departure) {
                    const now = new Date();
                    let nowMins = now.getHours() * 60 + now.getMinutes();
                    const [depH, depM] = nextDep.departure.split(':').map(Number);
                    const depMins = depH * 60 + depM;
                    if (nowMins < depMins && depMins - nowMins > 600) nowMins += 1440;

                    for (let i = 0; i < nextDep.stopTimes.length; i++) {
                      const timeStr = nextDep.stopTimes[i].time;
                      if (!timeStr) continue;
                      const [h, m] = timeStr.split(':').map(Number);
                      let stopMins = h * 60 + m;
                      if (stopMins < depMins) stopMins += 1440;
                      if (stopMins >= nowMins) {
                        nextStopIdx = i;
                        break;
                      }
                    }
                  }

                  return schedule.stops.map((stopName, stopIdx) => {
                    const time = nextDep?.stopTimes?.[stopIdx]?.time;
                    const isFirst = stopIdx === 0;
                    const isLast = stopIdx === schedule.stops.length - 1;
                    const isCurrent = nextDep?.isCurrent && stopIdx === nextStopIdx;

                    return (
                      <div key={stopIdx} className={`flex flex-col sm:flex-row gap-3 sm:gap-6 py-3 relative z-10 ${isFirst && !isCurrent ? 'opacity-60' : ''}`}>
                        <div className={`w-full sm:w-16 flex sm:flex-col justify-between sm:justify-start items-center sm:items-end pt-3 ${
                          isCurrent ? 'text-primary font-extrabold text-lg' : 'text-on-surface-variant text-sm'
                        }`}>
                          <span>{time || '—'}</span>
                          {isCurrent && (
                            <span className="text-primary text-xs sm:mt-1 flex items-center gap-0.5 font-bold animate-pulse">
                              <span className="material-symbols-outlined text-[14px]">directions_bus</span>След.
                            </span>
                          )}
                        </div>

                        <div className="hidden sm:flex items-start justify-center w-5 pt-3.5 flex-shrink-0">
                          <div className={`w-5 h-5 rounded-full border-2 transition-all flex items-center justify-center ${
                            isCurrent ? 'bg-primary/20 border-primary scale-110 shadow-lg' : 'bg-surface border-outline-variant'
                          }`}>
                            {isCurrent && <div className="w-2.5 h-2.5 rounded-full bg-primary animate-ping"></div>}
                            {isLast && <span className="material-symbols-outlined text-[13px] text-outline-variant">flag</span>}
                          </div>
                        </div>

                        <div className={`flex-grow rounded-xl p-3 border relative overflow-hidden transition-all ${
                          isCurrent ? 'bg-primary text-on-primary border-primary shadow-lg shadow-primary/20' :
                          isFirst ? 'bg-surface-container-high border-outline-variant/20 text-on-surface' :
                          'bg-surface-container-low border-outline-variant/10 text-on-surface'
                        }`}>
                          <h4 className={`font-semibold text-sm md:text-base ${isCurrent ? 'text-on-primary' : 'text-on-surface'}`}>
                            {stopName}
                          </h4>
                          {isFirst && <div className={`${isCurrent ? 'text-on-primary/80' : 'text-on-surface-variant'} text-xs mt-0.5`}>Начальная остановка</div>}
                          {isLast  && <div className={`${isCurrent ? 'text-on-primary/80' : 'text-tertiary'} text-xs mt-0.5`}>Конечная остановка</div>}
                        </div>
                      </div>
                    );
                  });
                })()}
              </div>

              {/* All departures */}
              {!loading && schedule.departures.length > 0 && (
                <div className="border-t border-outline-variant/30 p-4">
                  <h3 className="text-xs font-bold text-on-surface-variant uppercase tracking-wider mb-3">Все отправления</h3>
                  <div className="flex flex-wrap gap-1.5">
                    {schedule.departures.slice(0, 40).map((dep, i) => (
                      <span key={i} className={`text-xs font-semibold px-2.5 py-1 rounded-lg ${
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
