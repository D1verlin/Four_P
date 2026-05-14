import { useState, useEffect } from 'react';
import { notificationsApi } from '../api';

const typeConfig = {
  Disruption: {
    border: 'border-error',
    icon: 'warning',
    iconColor: 'text-error',
    label: 'Критический сбой',
    glow: 'absolute -right-4 -top-4 w-24 h-24 bg-error/10 rounded-full blur-2xl pointer-events-none'
  },
  Delay: {
    border: 'border-primary',
    icon: 'schedule',
    iconColor: 'text-primary',
    label: 'Задержка',
    glow: null
  },
  Info: {
    border: 'border-outline-variant',
    icon: 'info',
    iconColor: 'text-secondary',
    label: 'Информация',
    glow: null
  },
  Maintenance: {
    border: 'border-primary',
    icon: 'construction',
    iconColor: 'text-primary',
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
    { id: null, label: 'Все' },
    { id: 'Disruption', label: 'Сбои' },
    { id: 'Delay', label: 'Задержки' },
    { id: 'Maintenance', label: 'Плановые работы' },
  ];

  const handleFilter = (type) => {
    setActiveFilter(type);
    load(type);
  };

  return (
    <main className="flex-grow w-full max-w-7xl mx-auto px-margin-mobile md:px-margin-desktop py-md md:py-xl flex flex-col gap-lg pb-[100px] md:pb-lg">
      {}
      <header className="flex flex-col gap-sm">
        <h1 className="font-headline-xl text-headline-xl text-on-background">Центр уведомлений</h1>
        <p className="font-body-lg text-body-lg text-on-surface-variant max-w-2xl">
          Оперативная информация о состоянии транспортной сети, изменениях в расписании и экстренных ситуациях.
        </p>
        {}
        <div className="flex flex-wrap gap-xs mt-sm">
          {filters.map(f => (
            <button
              key={String(f.id)}
              id={`notif-filter-${f.id || 'all'}`}
              onClick={() => handleFilter(f.id)}
              className={`font-label-lg text-label-lg px-4 py-2 rounded-full transition-colors ${
                activeFilter === f.id
                  ? 'bg-primary-container text-on-primary-container'
                  : 'bg-surface-variant text-on-surface-variant hover:bg-surface-bright hover:text-on-surface'
              }`}
            >
              {f.label}
            </button>
          ))}
        </div>
      </header>

      {}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-md">
        {loading && (
          <div className="col-span-3 text-center py-12 text-on-surface-variant font-body-md text-body-md">
            Загрузка уведомлений...
          </div>
        )}
        {!loading && notifications.length === 0 && (
          <div className="col-span-3 bg-surface-container rounded-xl p-md text-center text-on-surface-variant font-body-md text-body-md">
            Нет уведомлений
          </div>
        )}
        {notifications.map((n, i) => {
          const cfg = typeConfig[n.type] || typeConfig.Info;
          const isWide = n.type === 'Maintenance';
          return (
            <div
              key={n.id}
              className={`bg-surface-container rounded-xl p-md flex flex-col gap-sm border-l-4 ${cfg.border} relative overflow-hidden group hover:bg-surface-container-high transition-colors ${isWide ? 'md:col-span-2 lg:col-span-2' : ''}`}
            >
              {cfg.glow && <div className={cfg.glow}></div>}
              <div className="flex justify-between items-start">
                <div className={`flex items-center gap-2 ${cfg.iconColor}`}>
                  <span className="material-symbols-outlined" style={{fontVariationSettings:"'FILL' 1"}}>{cfg.icon}</span>
                  <span className="font-label-lg text-label-lg uppercase tracking-wider">{cfg.label}</span>
                </div>
                <span className="font-label-sm text-label-sm text-on-surface-variant">{n.timeAgo}</span>
              </div>
              <h3 className="font-headline-md text-headline-md text-on-surface">{n.title}</h3>
              <p className="font-body-md text-body-md text-on-surface-variant">{n.message}</p>

              {}
              {n.affectedRoutes && (
                <div className="mt-auto pt-sm flex flex-wrap gap-2">
                  {routeIcons(n.affectedRoutes).map((r, ri) => (
                    <span key={ri} className="bg-surface-variant text-on-surface font-label-sm text-label-sm px-2 py-1 rounded-md flex items-center gap-1">
                      <span className="material-symbols-outlined text-[14px]">directions_bus</span>
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
