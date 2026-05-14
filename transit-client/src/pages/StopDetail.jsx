import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { stopsApi } from '../api';

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
    <main className="flex-grow w-full max-w-[1280px] mx-auto px-margin-mobile md:px-margin-desktop py-md flex flex-col gap-lg pb-[100px] md:pb-lg">
      {}
      <header className="flex flex-col gap-base">
        <div className="flex items-center gap-sm">
          <button className="text-on-surface-variant hover:text-on-surface transition-colors" onClick={() => navigate(-1)}>
            <span className="material-symbols-outlined">arrow_back</span>
          </button>
          <h1 className="font-headline-lg text-headline-lg text-on-surface">
            {stop ? `Остановка «${stop.name}»` : 'Загрузка...'}
          </h1>
        </div>
        {stop && (
          <p className="font-body-md text-body-md text-on-surface-variant pl-[44px]">
            Направление: {stop.direction} — {stop.address}
          </p>
        )}
      </header>

      {}
      <section className="flex flex-wrap gap-sm">
        <button
          id="filter-all"
          onClick={clearFilter}
          className={`font-label-lg text-label-lg px-4 py-2 rounded-full border flex items-center gap-xs transition-colors ${
            !activeType ? 'bg-primary text-on-primary border-primary' : 'bg-surface-variant text-on-surface-variant border-outline-variant hover:bg-surface-container-high'
          }`}
        >
          <span className="material-symbols-outlined text-[18px]" style={{fontVariationSettings: !activeType ? "'FILL' 1" : "'FILL' 0"}}>directions_bus</span>
          Все
        </button>
        {types.map(t => (
          <button
            key={t}
            id={`filter-${t.toLowerCase()}`}
            onClick={() => filterType(t)}
            className={`font-label-lg text-label-lg px-4 py-2 rounded-full border flex items-center gap-xs transition-colors ${
              activeType === t ? 'bg-primary text-on-primary border-primary' : 'bg-surface-variant text-on-surface-variant border-outline-variant hover:bg-surface-container-high'
            }`}
          >
            <span className="material-symbols-outlined text-[18px]">{transportIcons[t]}</span>
            {transportLabels[t]}
          </button>
        ))}
      </section>

      {}
      <section className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-gutter">
        {loading && (
          <div className="col-span-3 text-center text-on-surface-variant font-body-md text-body-md py-8">Загрузка...</div>
        )}
        {!loading && arrivals.length === 0 && (
          <div className="col-span-3 bg-surface-container rounded-xl p-md text-on-surface-variant font-body-md text-body-md text-center">
            Нет ближайших рейсов на этой остановке
          </div>
        )}
        {arrivals.map((arr, i) => (
          <article
            key={i}
            className={`bg-surface-container p-md rounded-xl border border-outline-variant flex flex-col gap-sm relative overflow-hidden ${arr.minutesUntil <= 2 ? '' : ''}`}
          >
            {arr.minutesUntil <= 2 && <div className="absolute top-0 left-0 w-1 h-full bg-primary"></div>}
            <div className="flex justify-between items-start">
              <div className="flex items-center gap-sm">
                <div className={`${pillColors[arr.type] || 'bg-surface-variant text-on-surface'} px-3 py-1 rounded-full font-headline-md text-headline-md flex items-center gap-xs`}>
                  <span className="material-symbols-outlined text-[20px]">{transportIcons[arr.type] || 'directions_bus'}</span>
                  {arr.routeNumber}
                </div>
                <div className="flex flex-col">
                  <span className="font-label-lg text-label-lg text-on-surface">{arr.routeName}</span>
                  <span className="font-label-sm text-label-sm text-on-surface-variant">{arr.arrivalTime}</span>
                </div>
              </div>
            </div>
            <div className="flex justify-between items-end mt-auto">
              <div className="flex flex-col">
                <span className={`font-headline-lg text-headline-lg ${arr.minutesUntil <= 2 ? 'text-primary' : timeColors[arr.type] || 'text-on-surface'}`}>
                  {arr.minutesUntil <= 0 ? 'Прибывает' : `${arr.minutesUntil} мин`}
                </span>
                <span className="font-label-sm text-label-sm text-on-surface-variant">
                  {arr.minutesUntil <= 0 ? 'Уже здесь' : `в ${arr.arrivalTime}`}
                </span>
              </div>
              <button className={`p-2 rounded-full bg-surface-variant hover:bg-surface-bright transition-colors flex items-center justify-center ${arr.minutesUntil <= 2 ? 'text-primary' : 'text-on-surface-variant hover:text-on-surface'}`}>
                <span className="material-symbols-outlined">{arr.minutesUntil <= 2 ? 'notifications' : 'notifications_none'}</span>
              </button>
            </div>
          </article>
        ))}
      </section>
    </main>
  );
}
