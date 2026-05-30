import { useState, useEffect } from 'react';
import { notificationsApi } from '../api';

const typeConfig = {
  Disruption: {
    border: 'border-error',
    icon: 'warning',
    iconColor: 'text-error',
    bg: 'bg-error/5',
    label: 'Критический сбой',
    glow: 'absolute -right-4 -top-4 w-24 h-24 bg-error/10 rounded-full blur-2xl pointer-events-none'
  },
  Delay: {
    border: 'border-primary',
    icon: 'schedule',
    iconColor: 'text-primary',
    bg: 'bg-primary/5',
    label: 'Задержка',
    glow: null
  },
  Info: {
    border: 'border-outline-variant',
    icon: 'info',
    iconColor: 'text-secondary',
    bg: '',
    label: 'Информация',
    glow: null
  },
  Maintenance: {
    border: 'border-tertiary',
    icon: 'construction',
    iconColor: 'text-tertiary',
    bg: 'bg-tertiary/5',
    label: 'Плановые работы',
    glow: null
  }
};

const routeIcons = (routeStr) => {
  if (!routeStr) return [];
  return routeStr.split(',').map(r => r.trim()).filter(Boolean);
};

export default function Notifications() {
  const [notifications, setNotifications] = useState([]);
  const [activeFilter, setActiveFilter] = useState(null);
  const [loading, setLoading] = useState(true);

  const load = async (type = null) => {
    setLoading(true);
    try {
      const r = await notificationsApi.getAll(type);
      setNotifications(r.data);
    } finally { setLoading(false); }
  };

  useEffect(() => { load(); }, []);

  const filters = [
    { id: null,          label: 'Все',             icon: 'notifications' },
    { id: 'Disruption',  label: 'Сбои',            icon: 'warning' },
    { id: 'Delay',       label: 'Задержки',        icon: 'schedule' },
    { id: 'Maintenance', label: 'Тех. работы',     icon: 'construction' },
  ];

  const handleFilter = (type) => {
    setActiveFilter(type);
    load(type);
  };

  return (
    <main className="flex-grow w-full max-w-[1280px] mx-auto px-4 md:px-8 py-4 md:py-6 flex flex-col gap-5 pb-[80px] md:pb-6">
      {/* Header */}
      <header className="flex flex-col gap-3">
        <h1 className="text-2xl md:text-3xl font-bold text-on-background">Центр уведомлений</h1>
        <p className="text-sm text-on-surface-variant max-w-xl">
          Оперативная информация о состоянии транспортной сети Бреста.
        </p>
        <div className="flex flex-wrap gap-2">
          {filters.map(f => (
            <button
              key={String(f.id)}
              id={`notif-filter-${f.id || 'all'}`}
              onClick={() => handleFilter(f.id)}
              className={`btn-chip text-sm font-semibold px-3 py-2 rounded-full flex items-center gap-1.5 ${
                activeFilter === f.id
                  ? 'bg-primary-container text-on-primary-container'
                  : 'bg-surface-variant text-on-surface-variant hover:bg-surface-bright hover:text-on-surface'
              }`}
            >
              <span className="material-symbols-outlined text-[16px]">{f.icon}</span>
              {f.label}
            </button>
          ))}
        </div>
      </header>

      {/* Cards grid */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-3 md:gap-4">
        {loading && (
          <div className="col-span-3 text-center py-16 text-on-surface-variant flex flex-col items-center gap-3">
            <span className="material-symbols-outlined animate-spin text-4xl text-outline">progress_activity</span>
            <span className="text-sm">Загрузка уведомлений...</span>
          </div>
        )}
        {!loading && notifications.length === 0 && (
          <div className="col-span-3 bg-surface-container rounded-2xl p-8 text-center text-on-surface-variant text-sm">
            <span className="material-symbols-outlined text-5xl block mb-3 text-outline">notifications_off</span>
            Нет уведомлений
          </div>
        )}
        {notifications.map((n) => {
          const cfg = typeConfig[n.type] || typeConfig.Info;
          const isWide = n.type === 'Maintenance';
          return (
            <div
              key={n.id}
              className={`card-hover bg-surface-container rounded-2xl p-4 flex flex-col gap-3 border-l-4 ${cfg.border} ${cfg.bg} relative overflow-hidden ${isWide ? 'md:col-span-2 lg:col-span-2' : ''}`}
            >
              {cfg.glow && <div className={cfg.glow}></div>}
              <div className="flex justify-between items-start gap-2">
                <div className={`flex items-center gap-2 ${cfg.iconColor}`}>
                  <span className="material-symbols-outlined text-[20px]" style={{fontVariationSettings:"'FILL' 1"}}>{cfg.icon}</span>
                  <span className="text-xs font-bold uppercase tracking-wider">{cfg.label}</span>
                </div>
                <span className="text-xs text-on-surface-variant flex-shrink-0">{n.timeAgo}</span>
              </div>
              <h3 className="text-base font-bold text-on-surface leading-tight">{n.title}</h3>
              <p className="text-sm text-on-surface-variant leading-relaxed">{n.message}</p>

              {n.affectedRoutes && (
                <div className="mt-auto pt-2 flex flex-wrap gap-1.5">
                  {routeIcons(n.affectedRoutes).map((r, ri) => (
                    <span key={ri} className="bg-surface-variant text-on-surface text-xs font-semibold px-2.5 py-1 rounded-lg flex items-center gap-1">
                      <span className="material-symbols-outlined text-[13px]">directions_bus</span>
                      Маршрут {r}
                    </span>
                  ))}
                </div>
              )}
            </div>
          );
        })}
      </div>
    </main>
  );
}
