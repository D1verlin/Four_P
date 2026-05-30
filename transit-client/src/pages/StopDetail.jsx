import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { stopsApi } from '../api';
import { useToast } from '../components/ToastContext';

const transportIcons = { Bus: 'directions_bus', Trolleybus: 'commute', Tram: 'tram', Minibus: 'airport_shuttle' };
const transportLabels = { Bus: 'Автобусы', Trolleybus: 'Троллейбусы', Tram: 'Трамваи', Minibus: 'Маршрутки' };
const pillColors = {
  Bus: 'bg-secondary text-on-secondary',
  Trolleybus: 'bg-tertiary-container text-on-tertiary-container',
  Tram: 'bg-tertiary text-on-tertiary',
  Minibus: 'bg-surface-variant text-on-surface'
};
const timeColors = {
  Bus: 'text-primary',
  Trolleybus: 'text-tertiary',
  Tram: 'text-secondary',
  Minibus: 'text-on-surface'
};

export default function StopDetail() {
  const { id } = useParams();
  const navigate = useNavigate();
  const { showToast } = useToast();
  const [stop, setStop] = useState(null);
  const [arrivals, setArrivals] = useState([]);
  const [activeType, setActiveType] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    Promise.all([
      stopsApi.getById(id),
      stopsApi.getArrivals(id)
    ]).then(([stopRes, arrivalsRes]) => {
      setStop(stopRes.data);
      setArrivals(arrivalsRes.data);
    }).finally(() => setLoading(false));
  }, [id]);

  const filterType = async (type) => {
    setActiveType(type);
    setLoading(true);
    try {
      const r = await stopsApi.getArrivals(id, type);
      setArrivals(r.data);
    } finally { setLoading(false); }
  };

  const clearFilter = async () => {
    setActiveType(null);
    setLoading(true);
    try {
      const r = await stopsApi.getArrivals(id);
      setArrivals(r.data);
    } finally { setLoading(false); }
  };

  const types = ['Bus', 'Tram', 'Minibus'];

  return (
    <main className="flex-grow w-full max-w-[1280px] mx-auto px-4 md:px-8 py-4 md:py-6 flex flex-col gap-5 pb-[80px] md:pb-6">
      {/* Header */}
      <header className="flex flex-col gap-2">
        <div className="flex items-center gap-3">
          <button
            className="btn-icon text-on-surface-variant hover:text-on-surface p-1.5 rounded-full hover:bg-surface-variant"
            onClick={() => navigate(-1)}
          >
            <span className="material-symbols-outlined">arrow_back</span>
          </button>
          <h1 className="text-xl md:text-2xl font-bold text-on-surface leading-tight">
            {stop ? `Остановка «${stop.name}»` : 'Загрузка...'}
          </h1>
          {stop && (
            <button
              className="btn-icon text-primary hover:text-primary-fixed ml-auto"
              onClick={() => {
                const saved = JSON.parse(localStorage.getItem('favStops') || '[]');
                if (!saved.includes(stop.id)) {
                  localStorage.setItem('favStops', JSON.stringify([...saved, stop.id]));
                  showToast('Остановка добавлена в избранное!', 'success');
                } else {
                  showToast('Остановка уже в избранном!', 'info');
                }
              }}
            >
              <span className="material-symbols-outlined">favorite_border</span>
            </button>
          )}
        </div>
      </header>

      {/* Filter chips */}
      <section className="flex flex-wrap gap-2">
        <button
          id="filter-all"
          onClick={clearFilter}
          className={`btn-chip text-sm font-semibold px-3 py-2 rounded-full border flex items-center gap-1.5 ${
            !activeType ? 'bg-primary text-on-primary border-primary' : 'bg-surface-variant text-on-surface-variant border-outline-variant hover:bg-surface-container-high'
          }`}
        >
          <span className="material-symbols-outlined text-[16px]" style={{fontVariationSettings: !activeType ? "'FILL' 1" : "'FILL' 0"}}>directions_bus</span>
          Все
        </button>
        {types.map(t => (
          <button
            key={t}
            id={`filter-${t.toLowerCase()}`}
            onClick={() => filterType(t)}
            className={`btn-chip text-sm font-semibold px-3 py-2 rounded-full border flex items-center gap-1.5 ${
              activeType === t ? 'bg-primary text-on-primary border-primary' : 'bg-surface-variant text-on-surface-variant border-outline-variant hover:bg-surface-container-high'
            }`}
          >
            <span className="material-symbols-outlined text-[16px]">{transportIcons[t]}</span>
            {transportLabels[t]}
          </button>
        ))}
      </section>

      {/* Arrivals grid */}
      <section className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-3">
        {loading && (
          <div className="col-span-3 text-center text-on-surface-variant py-12 flex flex-col items-center gap-3">
            <span className="material-symbols-outlined animate-spin text-4xl text-outline">progress_activity</span>
            <span className="text-sm">Загрузка...</span>
          </div>
        )}
        {!loading && arrivals.length === 0 && (
          <div className="col-span-3 bg-surface-container rounded-2xl p-8 text-on-surface-variant text-sm text-center">
            <span className="material-symbols-outlined text-5xl block mb-3 text-outline">directions_bus_filled</span>
            Нет ближайших рейсов на этой остановке
          </div>
        )}
        {arrivals.map((arr, i) => (
          <article
            key={i}
            className={`card-hover bg-surface-container p-4 rounded-2xl border border-outline-variant flex flex-col gap-3 relative overflow-hidden`}
          >
            {arr.minutesUntil <= 2 && <div className="absolute top-0 left-0 w-1 h-full bg-primary rounded-l-2xl"></div>}
            <div className="flex justify-between items-start gap-2">
              <div className="flex items-center gap-2.5">
                <div className={`${pillColors[arr.type] || 'bg-surface-variant text-on-surface'} px-3 py-1.5 rounded-xl font-bold text-sm flex items-center gap-1.5`}>
                  <span className="material-symbols-outlined text-[18px]">{transportIcons[arr.type] || 'directions_bus'}</span>
                  {arr.routeNumber}
                </div>
                <div className="flex flex-col min-w-0">
                  <span className="font-semibold text-sm text-on-surface truncate">{arr.routeName}</span>
                  <span className="text-xs text-on-surface-variant">{arr.arrivalTime}</span>
                </div>
              </div>
            </div>
            <div className="flex justify-between items-end mt-auto">
              <div className="flex flex-col">
                <span className={`text-2xl font-bold ${arr.minutesUntil <= 2 ? 'text-primary' : timeColors[arr.type] || 'text-on-surface'}`}>
                  {arr.minutesUntil <= 0 ? 'Прибывает' : `${arr.minutesUntil} мин`}
                </span>
                <span className="text-xs text-on-surface-variant">
                  {arr.minutesUntil <= 0 ? 'Уже здесь' : `в ${arr.arrivalTime}`}
                </span>
              </div>
              <button className={`btn-icon p-2 rounded-xl bg-surface-variant hover:bg-surface-bright flex items-center justify-center ${arr.minutesUntil <= 2 ? 'text-primary' : 'text-on-surface-variant hover:text-on-surface'}`}>
                <span className="material-symbols-outlined text-[20px]">{arr.minutesUntil <= 2 ? 'notifications' : 'notifications_none'}</span>
              </button>
            </div>
          </article>
        ))}
      </section>
    </main>
  );
}
