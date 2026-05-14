import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

export default function AdminLogin() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    try {
      const response = await axios.post('http://localhost:5000/api/auth/login', {
        username,
        password
      });
      localStorage.setItem('adminToken', response.data.token);
      localStorage.setItem('adminUser', JSON.stringify(response.data));
      navigate('/admin/dashboard');
    } catch (err) {
      setError(err.response?.data?.message || 'Ошибка авторизации');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="flex-1 flex items-center justify-center relative overflow-hidden bg-background">
      {}
      <div className="absolute top-[-10%] left-[-10%] w-[40%] h-[40%] bg-primary/10 rounded-full blur-[120px]"></div>
      <div className="absolute bottom-[-10%] right-[-10%] w-[40%] h-[40%] bg-tertiary/10 rounded-full blur-[120px]"></div>
      
      <div className="w-full max-w-md relative z-10 px-margin-mobile">
        <div className="bg-surface-container/60 backdrop-blur-xl border border-outline-variant/30 rounded-xl p-lg shadow-2xl">
          <div className="text-center mb-lg">
            <div className="inline-flex items-center justify-center w-20 h-20 bg-primary-container/30 text-primary rounded-full mb-md ring-4 ring-primary/10">
              <span className="material-symbols-outlined text-5xl">admin_panel_settings</span>
            </div>
            <h1 className="text-headline-lg text-on-surface tracking-tight">UrbanTransit</h1>
            <p className="text-label-lg text-on-surface-variant font-medium mt-xs uppercase tracking-widest">Admin Control Portal</p>
          </div>

          <form onSubmit={handleLogin} className="space-y-lg">
            {error && (
              <div className="p-md bg-error/10 border border-error/20 text-error rounded-lg text-label-lg flex items-center gap-base animate-in slide-in-from-top-1 duration-300">
                <span className="material-symbols-outlined text-lg">error</span>
                {error}
              </div>
            )}
            
            <div className="space-y-sm">
              <label className="text-label-lg text-on-surface-variant font-bold px-xs">ЛОГИН</label>
              <div className="relative group">
                <span className="material-symbols-outlined absolute left-md top-1/2 -translate-y-1/2 text-on-surface-variant group-focus-within:text-primary transition-colors">person</span>
                <input
                  type="text"
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                  className="w-full bg-surface-container-highest/50 border border-outline-variant rounded-lg py-md pl-xl pr-md text-on-surface focus:outline-none focus:border-primary focus:ring-4 focus:ring-primary/10 transition-all placeholder:text-outline"
                  placeholder="admin"
                  required
                />
              </div>
            </div>

            <div className="space-y-sm">
              <label className="text-label-lg text-on-surface-variant font-bold px-xs">ПАРОЛЬ</label>
              <div className="relative group">
                <span className="material-symbols-outlined absolute left-md top-1/2 -translate-y-1/2 text-on-surface-variant group-focus-within:text-primary transition-colors">lock</span>
                <input
                  type="password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  className="w-full bg-surface-container-highest/50 border border-outline-variant rounded-lg py-md pl-xl pr-md text-on-surface focus:outline-none focus:border-primary focus:ring-4 focus:ring-primary/10 transition-all placeholder:text-outline"
                  placeholder="••••••••"
                  required
                />
              </div>
            </div>

            <button
              type="submit"
              disabled={loading}
              className="w-full bg-primary text-on-primary py-md rounded-lg text-headline-md font-bold hover:brightness-110 active:scale-[0.98] transition-all disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center gap-base shadow-lg shadow-primary/20"
            >
              {loading ? (
                <span className="animate-spin material-symbols-outlined">progress_activity</span>
              ) : (
                <>Войти в систему <span className="material-symbols-outlined">arrow_forward</span></>
              )}
            </button>
          </form>
          
          <div className="mt-lg pt-lg border-t border-outline-variant/30 text-center">
            <p className="text-label-sm text-on-surface-variant font-medium">© 2024 UrbanTransit Systems • Secure Access Only</p>
          </div>
        </div>
      </div>
    </div>
  );
}
