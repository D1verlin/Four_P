import { BrowserRouter, Routes, Route, useLocation, Navigate } from 'react-router-dom';
import NavBar from './components/NavBar';
import RoutePlanner from './pages/RoutePlanner';
import Favorites from './pages/Favorites';
import StopDetail from './pages/StopDetail';
import Schedules from './pages/Schedules';
import Notifications from './pages/Notifications';
import AdminLogin from './pages/AdminLogin';
import AdminDashboard from './pages/AdminDashboard';
import { ToastProvider } from './components/ToastContext';

function AppContent() {
  const location = useLocation();
  const isAdminPage = location.pathname.startsWith('/admin');

  return (
    <div className="min-h-screen flex flex-col bg-background text-on-background">
      {!isAdminPage && <NavBar />}
      <Routes>
        <Route path="/" element={<RoutePlanner />} />
        <Route path="/favorites" element={<Favorites />} />
        <Route path="/stops/:id" element={<StopDetail />} />
        <Route path="/schedules" element={<Schedules />} />
        <Route path="/notifications" element={<Notifications />} />
        <Route path="/admin" element={<Navigate to="/admin/login" />} />
        <Route path="/admin/login" element={<AdminLogin />} />
        <Route path="/admin/dashboard" element={<AdminDashboard />} />
      </Routes>
    </div>
  );
}

export default function App() {
  return (
    <ToastProvider>
      <BrowserRouter>
        <AppContent />
      </BrowserRouter>
    </ToastProvider>
  );
}
