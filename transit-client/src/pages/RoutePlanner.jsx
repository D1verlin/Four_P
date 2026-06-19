import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { stopsApi, plannerApi } from '../api';

const transportIcons = { Bus: 'directions_bus', Trolleybus: 'commute', Tram: 'tram', Minibus: 'airport_shuttle' };
const transportLabels = { Bus: 'Автобус', Trolleybus: 'Троллейбус', Tram: 'Трамвай', Minibus: 'Маршрутка' };
const transportColors = {
  Bus: 'bg-secondary text-on-secondary',
  Trolleybus: 'bg-tertiary-container text-on-tertiary-container',
  Tram: 'bg-tertiary text-on-tertiary',
  Minibus: 'bg-surface-variant text-on-surface'
};

export default function RoutePlanner() {
  const [stops, setStops] = useState([]);
  const [fromId, setFromId] = useState('');
  const [toId, setToId] = useState('');
  const [fromSearch, setFromSearch] = useState('');
  const [toSearch, setToSearch] = useState('');
  const [fromSuggestions, setFromSuggestions] = useState([]);
  const [toSuggestions, setToSuggestions] = useState([]);
  const [results, setResults] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    stopsApi.getAll().then(r => setStops(r.data));
  }, []);

  const filterStops = (q) => {
    if (!q || q.length < 1) return [];
    const queryLower = q.toLowerCase();
    return stops.filter(s =>
      (s.name && s.name.toLowerCase().includes(queryLower)) ||
      (s.address && s.address.toLowerCase().includes(queryLower))
    ).slice(0, 6);
  };

  const handleFromInput = (v) => {
    setFromSearch(v); setFromId('');
    setFromSuggestions(v.length >= 1 ? filterStops(v) : []);
  };
  const handleToInput = (v) => {
    setToSearch(v); setToId('');
    setToSuggestions(v.length >= 1 ? filterStops(v) : []);
  };

  const selectFrom = (s) => { setFromId(s.id); setFromSearch(s.name); setFromSuggestions([]); };
  const selectTo = (s) => { setToId(s.id); setToSearch(s.name); setToSuggestions([]); };

  const handlePlan = async () => {
    let resolvedFromId = fromId;
    let resolvedToId = toId;
    if (!resolvedFromId && fromSearch) {
      const searchLower = fromSearch.toLowerCase();
      const match = stops.find(s => s.name && s.name.toLowerCase() === searchLower);
      if (match) resolvedFromId = match.id;
    }
    if (!resolvedToId && toSearch) {
      const searchLower = toSearch.toLowerCase();
      const match = stops.find(s => s.name && s.name.toLowerCase() === searchLower);
      if (match) resolvedToId = match.id;
    }
    if (!resolvedFromId || !resolvedToId) { setError('Выберите остановки из списка'); return; }
    if (resolvedFromId === resolvedToId) { setError('Остановки отправления и назначения совпадают'); return; }
    setError(''); setLoading(true); setResults(null);
    try {
      const r = await plannerApi.plan(resolvedFromId, resolvedToId);
      setResults(r.data);
    } catch { setError('Ошибка при поиске маршрутов'); }
    finally { setLoading(false); }
  };

  const swap = () => {
    setFromId(toId); setToId(fromId);
    setFromSearch(toSearch); setToSearch(fromSearch);
  };

  return (
    <main className="flex-grow w-full max-w-[1280px] mx-auto px-4 md:px-8 py-4 md:py-6 flex flex-col gap-4 md:gap-6 pb-[80px] md:pb-6">
      {/* Search card */}
      <section className="bg-surface-container-low rounded-2xl p-4 md:p-6 border border-outline-variant/30 relative z-10 shadow-xl animate-slide-up">
        <h1 className="text-xl md:text-3xl font-bold mb-4 text-on-surface tracking-tight">
          Планировщик маршрута
        </h1>
        <div className="flex flex-col gap-3 relative">
          {/* Vertical connector line */}
          <div className="absolute left-[20px] top-[44px] bottom-[44px] w-0.5 border-l-2 border-dashed border-outline-variant/50 hidden md:block"></div>

          {/* FROM */}
          <div className="flex items-center gap-3 relative">
            <div className="w-10 h-10 rounded-full bg-surface-container-high flex items-center justify-center border border-outline-variant flex-shrink-0 z-10">
              <span className="material-symbols-outlined text-on-surface-variant text-[20px]">radio_button_checked</span>
            </div>
            <div className="flex-grow relative">
              <input
                id="from-input"
                className="w-full bg-surface-container h-12 rounded-xl pl-4 pr-4 border border-outline-variant/50 focus:border-primary focus:ring-2 focus:ring-primary/20 text-on-surface text-sm md:text-base placeholder-on-surface-variant/50 outline-none transition-all"
                placeholder="Пункт отправления"
                value={fromSearch}
                onChange={e => handleFromInput(e.target.value)}
                autoComplete="off"
              />
              {fromSuggestions.length > 0 && (
                <div className="absolute top-full left-0 right-0 bg-surface-container-high border border-outline-variant rounded-xl mt-1 z-50 shadow-2xl animate-scale-in overflow-hidden">
                  {fromSuggestions.map(s => (
                    <button key={s.id} onClick={() => selectFrom(s)}
                      className="w-full text-left px-4 py-2.5 hover:bg-surface-container-highest flex items-center gap-3 transition-colors first:rounded-t-xl last:rounded-b-xl">
                      <span className="material-symbols-outlined text-[18px] text-on-surface-variant flex-shrink-0">location_on</span>
                      <div>
                        <div className="font-semibold text-sm text-on-surface">{s.name}</div>
                        <div className="text-xs text-on-surface-variant">{s.address}</div>
                      </div>
                    </button>
                  ))}
                </div>
              )}
            </div>
            <button onClick={swap} className="btn-icon text-on-surface-variant hover:text-on-surface p-2 rounded-full hover:bg-surface-variant flex-shrink-0">
              <span className="material-symbols-outlined">swap_vert</span>
            </button>
          </div>

          {/* TO */}
          <div className="flex items-center gap-3 relative">
            <div className="w-10 h-10 rounded-full bg-surface-container-high flex items-center justify-center border border-outline-variant flex-shrink-0 z-10">
              <span className="material-symbols-outlined text-primary text-[20px]">location_on</span>
            </div>
            <div className="flex-grow relative">
              <input
                id="to-input"
                className="w-full bg-surface-container h-12 rounded-xl pl-4 pr-4 border border-outline-variant/50 focus:border-primary focus:ring-2 focus:ring-primary/20 text-on-surface text-sm md:text-base placeholder-on-surface-variant/50 outline-none transition-all"
                placeholder="Пункт назначения"
                value={toSearch}
                onChange={e => handleToInput(e.target.value)}
                autoComplete="off"
              />
              {toSuggestions.length > 0 && (
                <div className="absolute top-full left-0 right-0 bg-surface-container-high border border-outline-variant rounded-xl mt-1 z-50 shadow-2xl animate-scale-in overflow-hidden">
                  {toSuggestions.map(s => (
                    <button key={s.id} onClick={() => selectTo(s)}
                      className="w-full text-left px-4 py-2.5 hover:bg-surface-container-highest flex items-center gap-3 transition-colors first:rounded-t-xl last:rounded-b-xl">
                      <span className="material-symbols-outlined text-[18px] text-on-surface-variant flex-shrink-0">location_on</span>
                      <div>
                        <div className="font-semibold text-sm text-on-surface">{s.name}</div>
                        <div className="text-xs text-on-surface-variant">{s.address}</div>
                      </div>
                    </button>
                  ))}
                </div>
              )}
            </div>
          </div>
        </div>

        {error && <p className="text-error text-xs mt-3 flex items-center gap-1"><span className="material-symbols-outlined text-[16px]">error</span>{error}</p>}

        <div className="mt-4 flex flex-col sm:flex-row gap-3 items-stretch sm:items-center justify-between">
          <button className="btn-chip bg-surface-variant text-on-surface-variant hover:bg-surface-container-high font-semibold text-sm px-4 h-10 rounded-xl flex items-center justify-center gap-2">
            <span className="material-symbols-outlined text-[18px]">schedule</span>Сейчас
          </button>
          <button
            id="find-routes-btn"
            onClick={handlePlan}
            disabled={loading}
            className="btn-primary bg-primary text-on-primary font-bold text-sm px-6 h-12 rounded-xl flex items-center justify-center gap-2 shadow-lg disabled:opacity-50 disabled:cursor-not-allowed"
          >
            {loading
              ? <><span className="material-symbols-outlined animate-spin text-[18px]">progress_activity</span>Поиск...</>
              : <><span className="material-symbols-outlined text-[18px]">search</span>Найти маршруты</>
            }
          </button>
        </div>
      </section>

      {/* Results */}
      {results && (
        <div className="flex flex-col gap-3 animate-slide-up">
          <h2 className="text-lg md:text-2xl font-bold text-on-surface">
            {results.from} <span className="text-primary">→</span> {results.to}
          </h2>

          {results.message && (
            <div className="bg-surface-container rounded-xl p-4 border border-outline-variant/30 text-on-surface-variant text-sm">
              {results.message}
            </div>
          )}

          {results.routes?.map((route, i) => (
            <div key={i} className="card-hover bg-surface-container-lowest border border-outline-variant/40 rounded-2xl p-4 md:p-5 flex flex-col gap-3 hover:border-secondary/50">
              {/* Route header */}
              <div className="flex flex-wrap justify-between items-start gap-3 border-b border-outline-variant/20 pb-3">
                <div className="flex flex-col">
                  <div className="flex items-baseline gap-1.5">
                    <span className="text-3xl font-bold text-primary">{route.travelMinutes}</span>
                    <span className="text-sm text-on-surface-variant font-medium">мин</span>
                  </div>
                  <span className="text-xs text-on-surface-variant mt-0.5">
                    {route.arrivalTime ? `Прибытие в ${route.arrivalTime}` : 'Нет рейсов'}
                  </span>
                </div>
                <div className="flex flex-wrap gap-2 items-center">
                  <div className={`${transportColors[route.type] || 'bg-surface-variant text-on-surface'} text-sm font-semibold px-3 py-1.5 rounded-lg flex items-center gap-1.5 shadow-sm`}>
                    <span className="material-symbols-outlined text-[16px]">{transportIcons[route.type]}</span>
                    {transportLabels[route.type]} {route.routeNumber}
                  </div>
                  {route.minutesUntil != null && (
                    <span className="text-primary font-semibold text-sm bg-primary/10 px-3 py-1.5 rounded-lg">
                      {route.minutesUntil === 0 ? '🚌 Сейчас' : `через ${route.minutesUntil} мин`}
                    </span>
                  )}
                </div>
              </div>

              {/* Stops timeline */}
              <div className="flex flex-col gap-2.5 pl-5 border-l-2 border-surface-variant ml-1">
                {route.stops?.map((stop, si) => {
                  const isFirst = si === 0;
                  const isLast = si === route.stops.length - 1;
                  return (
                    <div key={si} className="relative">
                      <div className={`absolute -left-[25px] top-1.5 w-3 h-3 rounded-full border-2 ${
                        isFirst ? 'bg-surface-container-high border-outline-variant' :
                        isLast  ? 'bg-primary border-primary' :
                        'bg-secondary border-secondary'
                      }`}></div>
                      <div className="flex justify-between items-center gap-2">
                        <span className={`text-sm ${isFirst || isLast ? 'font-semibold text-on-surface' : 'text-on-surface-variant'}`}>
                          {isFirst ? '🚏 ' : isLast ? '🏁 ' : ''}{stop.name}
                        </span>
                        {stop.time && <span className="text-tertiary text-xs font-semibold flex-shrink-0">{stop.time}</span>}
                      </div>
                    </div>
                  );
                })}
              </div>

              <button
                onClick={() => navigate(`/schedules?routeId=${route.routeId}`)}
                className="text-primary text-sm font-semibold flex items-center gap-1 hover:underline w-fit"
              >
                Полное расписание <span className="material-symbols-outlined text-[16px]">arrow_forward</span>
              </button>
            </div>
          ))}
        </div>
      )}
    </main>
  );
}
