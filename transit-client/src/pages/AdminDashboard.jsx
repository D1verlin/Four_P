import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const TABLES = [
  { id: 'stops', name: 'Остановки', icon: 'location_on', color: 'primary' },
  { id: 'routes', name: 'Маршруты', icon: 'directions_bus', color: 'secondary' },
  { id: 'schedules', name: 'Расписание', icon: 'schedule', color: 'tertiary' },
  { id: 'notifications', name: 'Уведомления', icon: 'notifications', color: 'error' },
  { id: 'users', name: 'Пользователи', icon: 'group', color: 'primary' },
];

export default function AdminDashboard() {
  const [activeTab, setActiveTab] = useState('stops');
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [editingItem, setEditingItem] = useState(null);
  const [formData, setFormData] = useState({});
  const [stats, setStats] = useState({});
  const navigate = useNavigate();

  const token = localStorage.getItem('adminToken');

  useEffect(() => {
    if (!token) {
      navigate('/admin/login');
      return;
    }
    fetchData();
    fetchStats();
  }, [activeTab, token]);

  const fetchData = async () => {
    setLoading(true);
    try {
      const response = await axios.get(`http://localhost:5000/api/admin/${activeTab}`, {
        headers: { Authorization: `Bearer ${token}` }
      });
      setData(response.data);
    } catch (err) {
      console.error(err);
      if (err.response?.status === 401) navigate('/admin/login');
    } finally {
      setLoading(false);
    }
  };

  const fetchStats = async () => {
    
    setStats({
      totalRoutes: 7,
      activeStops: 16,
      pendingNotifications: 4,
      totalUsers: 2
    });
  };

  const handleDelete = async (id) => {
    if (!window.confirm('Вы уверены, что хотите удалить этот элемент?')) return;
    try {
      await axios.delete(`http://localhost:5000/api/admin/${activeTab}/${id}`, {
        headers: { Authorization: `Bearer ${token}` }
      });
      fetchData();
    } catch (err) {
      alert('Ошибка при удалении');
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (editingItem) {
        await axios.put(`http://localhost:5000/api/admin/${activeTab}/${editingItem.id}`, formData, {
          headers: { Authorization: `Bearer ${token}` }
        });
      } else {
        await axios.post(`http://localhost:5000/api/admin/${activeTab}`, formData, {
          headers: { Authorization: `Bearer ${token}` }
        });
      }
      setShowModal(false);
      setEditingItem(null);
      setFormData({});
      fetchData();
    } catch (err) {
      alert('Ошибка при сохранении');
    }
  };

  const openEdit = (item) => {
    setEditingItem(item);
    setFormData(item);
    setShowModal(true);
  };

  const openCreate = () => {
    setEditingItem(null);
    setFormData({});
    setShowModal(true);
  };

  const renderTable = () => {
    if (loading) return (
      <div className="p-xl flex flex-col items-center justify-center gap-md">
        <span className="animate-spin material-symbols-outlined text-4xl text-primary">progress_activity</span>
        <p className="text-label-lg text-on-surface-variant animate-pulse">Загрузка данных...</p>
      </div>
    );
    if (data.length === 0) return (
      <div className="p-xl text-center space-y-md">
        <span className="material-symbols-outlined text-6xl text-outline-variant">inventory_2</span>
        <p className="text-body-lg text-on-surface-variant">Нет данных для отображения</p>
      </div>
    );

    const keys = data.length > 0 ? Object.keys(data[0]) : [];

    return (
      <div className="overflow-x-auto">
        <table className="w-full text-left border-collapse">
          <thead>
            <tr className="bg-surface-container-highest/30 border-b border-outline-variant/30">
              {keys.map(key => (
                <th key={key} className="p-md text-label-sm font-bold text-on-surface-variant uppercase tracking-widest">{key}</th>
              ))}
              <th className="p-md text-label-sm font-bold text-on-surface-variant uppercase tracking-widest text-right">Управление</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-outline-variant/20">
            {data.map((item, idx) => (
              <tr key={idx} className="hover:bg-primary/5 transition-all group">
                {keys.map(key => (
                  <td key={key} className="p-md text-body-md text-on-surface">
                    {typeof item[key] === 'boolean' ? (
                      <span className={`px-sm py-xs rounded-full text-label-sm font-bold ${item[key] ? 'bg-tertiary/20 text-tertiary' : 'bg-error/20 text-error'}`}>
                        {item[key] ? 'TRUE' : 'FALSE'}
                      </span>
                    ) : String(item[key])}
                  </td>
                ))}
                <td className="p-md text-right space-x-sm whitespace-nowrap">
                  {activeTab !== 'users' && activeTab !== 'schedules' && (
                    <button onClick={() => openEdit(item)} className="w-10 h-10 inline-flex items-center justify-center bg-surface-container-high text-on-surface-variant hover:bg-primary hover:text-on-primary rounded-lg transition-all shadow-sm">
                      <span className="material-symbols-outlined text-lg">edit</span>
                    </button>
                  )}
                  <button onClick={() => handleDelete(item.id)} className="w-10 h-10 inline-flex items-center justify-center bg-surface-container-high text-on-surface-variant hover:bg-error hover:text-on-error rounded-lg transition-all shadow-sm">
                    <span className="material-symbols-outlined text-lg">delete</span>
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  };

  return (
    <div className="flex-1 flex flex-col md:flex-row bg-background font-body-md h-screen overflow-hidden">
      {}
      <aside className="w-full md:w-72 bg-surface-container-low border-b md:border-b-0 md:border-r border-outline-variant/30 flex flex-col shadow-2xl z-20">
        <div className="p-lg mb-md flex items-center gap-md">
          <div className="w-12 h-12 bg-primary flex items-center justify-center rounded-xl shadow-lg shadow-primary/20">
            <span className="material-symbols-outlined text-on-primary text-3xl">route</span>
          </div>
          <div>
            <h2 className="text-headline-md text-on-surface font-bold tracking-tight">UrbanTransit</h2>
            <p className="text-label-sm text-primary font-bold uppercase tracking-widest opacity-80">Management</p>
          </div>
        </div>

        <nav className="flex-1 px-md space-y-sm overflow-y-auto">
          {TABLES.map(tab => (
            <button
              key={tab.id}
              onClick={() => setActiveTab(tab.id)}
              className={`w-full flex items-center gap-md px-md py-md rounded-xl transition-all group ${
                activeTab === tab.id 
                  ? 'bg-primary text-on-primary shadow-lg shadow-primary/20 scale-[1.02]' 
                  : 'text-on-surface-variant hover:bg-surface-container-highest hover:text-on-surface'
              }`}
            >
              <span className={`material-symbols-outlined ${activeTab === tab.id ? '' : 'group-hover:scale-110 transition-transform'}`}>{tab.icon}</span>
              <span className="font-bold tracking-tight">{tab.name}</span>
              {activeTab === tab.id && <span className="ml-auto w-1.5 h-6 bg-on-primary/30 rounded-full animate-pulse"></span>}
            </button>
          ))}
        </nav>

        <div className="p-lg border-t border-outline-variant/30 bg-surface-container-lowest/50 backdrop-blur-md">
          <button
            onClick={() => { localStorage.removeItem('adminToken'); navigate('/admin/login'); }}
            className="w-full flex items-center gap-md px-md py-md rounded-xl text-error hover:bg-error/10 font-bold transition-all group"
          >
            <span className="material-symbols-outlined group-hover:rotate-12 transition-transform">logout</span>
            <span>Завершить сеанс</span>
          </button>
        </div>
      </aside>

      {}
      <main className="flex-1 flex flex-col relative overflow-hidden">
        {}
        <header className="sticky top-0 z-10 bg-background/60 backdrop-blur-xl border-b border-outline-variant/20 px-gutter md:px-lg py-md flex items-center justify-between">
          <div>
            <h1 className="text-headline-md text-on-surface capitalize">{TABLES.find(t => t.id === activeTab).name}</h1>
            <p className="text-label-lg text-on-surface-variant font-medium">UrbanTransit Control • {activeTab.toUpperCase()}</p>
          </div>
          <div className="flex items-center gap-md">
            <div className="hidden md:flex flex-col items-end px-md border-r border-outline-variant/30">
              <p className="text-label-sm text-on-surface-variant font-bold">АДМИНИСТРАТОР</p>
              <p className="text-label-lg text-on-surface font-bold">admin@urban.local</p>
            </div>
            <button className="w-12 h-12 bg-surface-container-highest rounded-full flex items-center justify-center text-on-surface border border-outline-variant/30 hover:border-primary transition-colors">
              <span className="material-symbols-outlined">account_circle</span>
            </button>
          </div>
        </header>

        <div className="flex-1 p-gutter md:p-lg overflow-y-auto space-y-lg custom-scrollbar">
          {}
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-md">
            <StatCard icon="directions_bus" label="Маршрутов" value={stats.totalRoutes} color="bg-primary/10 text-primary border-primary/20" />
            <StatCard icon="location_on" label="Остановок" value={stats.activeStops} color="bg-tertiary/10 text-tertiary border-tertiary/20" />
            <StatCard icon="notification_important" label="Уведомлений" value={stats.pendingNotifications} color="bg-error/10 text-error border-error/20" />
            <StatCard icon="group" label="Пользователей" value={stats.totalUsers} color="bg-secondary/10 text-secondary border-secondary/20" />
          </div>

          {}
          <div className="space-y-md">
            <div className="flex items-center justify-between">
              <div className="flex items-center gap-base">
                <span className="w-2 h-8 bg-primary rounded-full"></span>
                <h3 className="text-headline-md text-on-surface">Управление записями</h3>
              </div>
              {activeTab !== 'users' && activeTab !== 'schedules' && (
                <button 
                  onClick={openCreate}
                  className="bg-primary text-on-primary px-lg py-md rounded-xl font-bold flex items-center gap-base shadow-xl shadow-primary/20 hover:scale-105 active:scale-95 transition-all"
                >
                  <span className="material-symbols-outlined">add</span>
                  Добавить новую запись
                </button>
              )}
            </div>

            <div className="bg-surface-container-low rounded-xl border border-outline-variant/30 overflow-hidden shadow-sm relative group">
              <div className="absolute inset-0 bg-gradient-to-br from-primary/5 via-transparent to-tertiary/5 pointer-events-none"></div>
              {renderTable()}
            </div>
          </div>
        </div>
      </main>

      {}
      {showModal && (
        <div className="fixed inset-0 z-50 flex items-center justify-center p-md bg-background/90 backdrop-blur-md animate-in fade-in duration-300">
          <div className="bg-surface-container-high w-full max-w-xl rounded-2xl border border-outline-variant/50 shadow-2xl overflow-hidden animate-in zoom-in-95 duration-200">
            <div className="p-lg bg-surface-container-highest border-b border-outline-variant/30 flex items-center justify-between">
              <div>
                <h3 className="text-headline-md text-on-surface">{editingItem ? 'Редактирование' : 'Создание записи'}</h3>
                <p className="text-label-lg text-primary font-bold uppercase tracking-widest">{activeTab}</p>
              </div>
              <button onClick={() => setShowModal(false)} className="w-10 h-10 flex items-center justify-center bg-surface-container text-on-surface-variant hover:text-error rounded-full transition-all">
                <span className="material-symbols-outlined">close</span>
              </button>
            </div>
            <form onSubmit={handleSubmit} className="p-lg space-y-md max-h-[70vh] overflow-y-auto custom-scrollbar">
              {Object.keys(data[0] || {}).filter(k => k !== 'id' && k !== 'createdAt').map(key => (
                <div key={key} className="space-y-xs group">
                  <label className="text-label-lg text-on-surface-variant font-bold px-xs group-focus-within:text-primary transition-colors lowercase tracking-wider">{key}</label>
                  <input
                    type="text"
                    value={formData[key] || ''}
                    onChange={(e) => setFormData({ ...formData, [key]: e.target.value })}
                    className="w-full bg-surface-container-highest/50 border border-outline-variant rounded-lg py-md px-md text-on-surface focus:outline-none focus:border-primary focus:ring-4 focus:ring-primary/10 transition-all"
                    required
                  />
                </div>
              ))}
              <div className="pt-lg flex justify-end gap-md sticky bottom-0 bg-surface-container-high mt-lg border-t border-outline-variant/20 pt-lg">
                <button type="button" onClick={() => setShowModal(false)} className="px-lg py-md text-on-surface-variant font-bold hover:text-on-surface transition-colors">Отменить</button>
                <button type="submit" className="bg-primary text-on-primary px-xl py-md rounded-xl font-bold shadow-lg shadow-primary/20 hover:brightness-110 transition-all">Сохранить изменения</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
}

function StatCard({ icon, label, value, color }) {
  return (
    <div className={`p-lg rounded-xl border flex items-center gap-lg transition-all hover:scale-[1.02] cursor-default bg-surface-container-low ${color}`}>
      <div className={`w-14 h-14 rounded-xl flex items-center justify-center shadow-inner ${color.replace('border-', 'bg-opacity-20 ')}`}>
        <span className="material-symbols-outlined text-4xl">{icon}</span>
      </div>
      <div>
        <p className="text-label-sm font-bold uppercase tracking-widest opacity-70">{label}</p>
        <p className="text-headline-lg font-bold leading-none mt-xs">{value}</p>
      </div>
    </div>
  );
}
