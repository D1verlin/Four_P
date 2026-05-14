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
    return stops.filter(s =>
      s.name.toLowerCase().includes(q.toLowerCase()) ||
      s.address.toLowerCase().includes(q.toLowerCase())
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
      const match = stops.find(s => s.name.toLowerCase() === fromSearch.toLowerCase());
      if (match) resolvedFromId = match.id;
    }
    if (!resolvedToId && toSearch) {
      const match = stops.find(s => s.name.toLowerCase() === toSearch.toLowerCase());
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
    <main className="flex-grow w-full max-w-[1280px] mx-auto px-margin-mobile md:px-margin-desktop py-md md:py-lg flex flex-col gap-lg pb-[100px] md:pb-md">
      {}
      <section className="bg-surface-container-low rounded-xl p-md border border-outline-variant/30 relative z-10 shadow-lg">
        <h1 className="font-headline-lg text-headline-lg mb-md text-on-surface">Планировщик маршрута</h1>
        <div className="flex flex-col gap-sm relative">
          {}
          <div className="absolute left-[20px] top-[40px] bottom-[40px] w-0.5 border-l-2 border-dashed border-outline-variant/50 hidden md:block"></div>

          {}
          <div className="flex items-center gap-sm relative">
            <div className="w-10 h-10 rounded-full bg-surface-container-high flex items-center justify-center border border-outline-variant flex-shrink-0 z-10">
              <span className="material-symbols-outlined text-on-surface-variant">radio_button_checked</span>
            </div>
            <div className="flex-grow relative">
              <input
                id="from-input"
                className="w-full bg-surface-container h-12 rounded-lg pl-sm pr-sm border border-outline-variant/50 focus:border-primary focus:ring-1 focus:ring-primary text-on-surface font-body-lg text-body-lg placeholder-on-surface-variant/50 outline-none transition-colors"
                placeholder="Пункт отправления"
                value={fromSearch}
                onChange={e => handleFromInput(e.target.value)}
                autoComplete="off"
              />
              {fromSuggestions.length > 0 && (
                <div className="absolute top-full left-0 right-0 bg-surface-container-high border border-outline-variant rounded-lg mt-1 z-50 shadow-xl">
                  {fromSuggestions.map(s => (
                    <button key={s.id} onClick={() => selectFrom(s)}
                      className="w-full text-left px-sm py-2 hover:bg-surface-container-highest flex items-center gap-2 transition-colors first:rounded-t-lg last:rounded-b-lg">
                      <span className="material-symbols-outlined text-[18px] text-on-surface-variant">location_on</span>
                      <div>
                        <div className="font-label-lg text-label-lg text-on-surface">{s.name}</div>
                        <div className="font-label-sm text-label-sm text-on-surface-variant">{s.address}</div>
                      </div>
                    </button>
                  ))}
                </div>
              )}
            </div>
            <button onClick={swap} className="text-on-surface-variant hover:text-on-surface transition-colors p-2 rounded-full hover:bg-surface-variant flex-shrink-0">
              <span className="material-symbols-outlined">swap_vert</span>
            </button>
          </div>

          {}
          <div className="flex items-center gap-sm relative">
            <div className="w-10 h-10 rounded-full bg-surface-container-high flex items-center justify-center border border-outline-variant flex-shrink-0 z-10">
              <span className="material-symbols-outlined text-primary">location_on</span>
            </div>
            <div className="flex-grow relative">
              <input
                id="to-input"
                className="w-full bg-surface-container h-12 rounded-lg pl-sm pr-sm border border-outline-variant/50 focus:border-primary focus:ring-1 focus:ring-primary text-on-surface font-body-lg text-body-lg placeholder-on-surface-variant/50 outline-none transition-colors"
                placeholder="Пункт назначения"
                value={toSearch}
                onChange={e => handleToInput(e.target.value)}
                autoComplete="off"
              />
              {toSuggestions.length > 0 && (
                <div className="absolute top-full left-0 right-0 bg-surface-container-high border border-outline-variant rounded-lg mt-1 z-50 shadow-xl">
                  {toSuggestions.map(s => (
                    <button key={s.id} onClick={() => selectTo(s)}
                      className="w-full text-left px-sm py-2 hover:bg-surface-container-highest flex items-center gap-2 transition-colors first:rounded-t-lg last:rounded-b-lg">
                      <span className="material-symbols-outlined text-[18px] text-on-surface-variant">location_on</span>
                      <div>
                        <div className="font-label-lg text-label-lg text-on-surface">{s.name}</div>
                        <div className="font-label-sm text-label-sm text-on-surface-variant">{s.address}</div>
                      </div>
                    </button>
                  ))}
                </div>
              )}
            </div>
          </div>
        </div>

        {error && <p className="text-error font-label-sm text-label-sm mt-sm">{error}</p>}

        <div className="mt-md flex flex-wrap gap-sm items-center justify-between">
          <div className="flex gap-sm">
            <button className="bg-primary text-on-primary font-label-lg text-label-lg px-sm h-10 rounded-lg flex items-center gap-xs hover:bg-primary-fixed transition-colors">
              <span className="material-symbols-outlined text-[18px]">schedule</span>Сейчас
            </button>
          </div>
          <button
            id="find-routes-btn"
            onClick={handlePlan}
            disabled={loading}
            className="bg-secondary text-on-secondary font-label-lg text-label-lg px-md h-12 rounded-lg flex items-center gap-xs hover:bg-secondary-fixed transition-colors w-full md:w-auto justify-center mt-sm md:mt-0 shadow-md disabled:opacity-50"
          >
            {loading ? 'Поиск...' : 'Найти маршруты'}
            <span className="material-symbols-outlined">arrow_forward</span>
          </button>
        </div>
      </section>

      {}
      {results && (
        <div className="flex flex-col gap-sm">
          <h2 className="font-headline-md text-headline-md text-on-surface mb-xs">
            {results.from} → {results.to}
          </h2>

          {results.message && (
            <div className="bg-surface-container rounded-xl p-md border border-outline-variant/30 text-on-surface-variant font-body-md text-body-md">
              {results.message}
            </div>
          )}

          {results.routes?.map((route, i) => (
            <div key={i} className="bg-surface-container-lowest border border-outline-variant/40 rounded-xl p-sm md:p-md flex flex-col gap-sm hover:border-secondary transition-colors group">
              {}
              <div className="flex justify-between items-start border-b border-outline-variant/20 pb-sm mb-xs">
                <div className="flex flex-col">
                  <div className="flex items-baseline gap-xs">
                    <span className="font-headline-xl text-headline-xl text-primary">{route.travelMinutes}</span>
                    <span className="font-label-lg text-label-lg text-on-surface-variant">мин</span>
                  </div>
                  <span className="font-body-md text-body-md text-on-surface-variant">
                    {route.arrivalTime ? `Прибытие в ${route.arrivalTime}` : 'Нет рейсов'}
                  </span>
                </div>
                <div className="flex flex-wrap gap-xs justify-end">
                  <div className={`${transportColors[route.type] || 'bg-surface-variant text-on-surface'} font-label-lg text-label-lg px-2 py-1 rounded flex items-center gap-1 shadow-sm`}>
                    <span className="material-symbols-outlined text-[16px]">{transportIcons[route.type]}</span>
                    {transportLabels[route.type]} {route.routeNumber}
                  </div>
                  {route.minutesUntil != null && (
                    <span className="text-primary font-label-lg text-label-lg self-center text-sm">
                      {route.minutesUntil === 0 ? 'Сейчас' : `через ${route.minutesUntil} мин`}
                    </span>
                  )}
                </div>
              </div>

              {}
              <div className="flex flex-col gap-3 font-body-md text-body-md text-on-surface relative pl-6 border-l-2 border-surface-variant ml-2">
                {route.stops?.map((stop, si) => {
                  const isFirst = si === 0;
                  const isLast = si === route.stops.length - 1;
                  return (
                    <div key={si} className="relative">
                      <div className={`absolute -left-[31px] top-1.5 w-3 h-3 rounded-full border-2 ${
                        isFirst ? 'bg-surface-container-high border-outline-variant' :
                        isLast ? 'bg-primary border-primary' :
                        'bg-secondary border-secondary'
                      }`}></div>
                      <div className="flex justify-between items-start">
                        <div>
                          <span className={`font-label-lg text-label-lg block ${isFirst || isLast ? 'text-on-surface' : 'text-on-surface-variant'}`}>
                            {isFirst ? '🚏 ' : isLast ? '🏁 ' : ''}{stop.name}
                          </span>
                        </div>
                        {stop.time && <span className="text-tertiary font-label-lg text-label-lg ml-2 flex-shrink-0">{stop.time}</span>}
                      </div>
                    </div>
                  );
                })}
              </div>

              <button
                onClick={() => navigate(`/schedules?routeId=${route.routeId}`)}
                className="text-primary font-label-lg text-label-lg flex items-center gap-1 hover:underline w-fit mt-1"
              >
                Полное расписание <span className="material-symbols-outlined text-[18px]">arrow_forward</span>
              </button>
            </div>
          ))}
        </div>
      )}
    </main>
  );
}
