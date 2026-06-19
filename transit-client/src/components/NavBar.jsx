import { Link, useLocation } from 'react-router-dom';

const navLinks = [
  { to: '/', label: 'Планировщик', icon: 'directions' },
  { to: '/favorites', label: 'Избранное', icon: 'star' },
  { to: '/schedules', label: 'Расписания', icon: 'schedule' },
  { to: '/notifications', label: 'Уведомления', icon: 'notifications' },
];

export default function NavBar() {
  const { pathname } = useLocation();

  return (
    <nav className="sticky top-0 z-50 flex justify-between items-center px-4 md:px-6 py-3 w-full bg-white/80 backdrop-blur-md border-b border-zinc-200 font-sans antialiased">
      <div className="flex items-center gap-4 md:gap-6">
        <Link to="/" className="text-lg md:text-xl font-bold tracking-tight text-zinc-900 flex items-center gap-2 shrink-0">
          <span className="material-symbols-outlined text-primary" style={{fontVariationSettings:"'FILL' 1"}}>directions_transit</span>
          <span className="hidden sm:inline">БрестТранзит</span>
        </Link>
        <div className="hidden md:flex gap-1">
          {navLinks.map(link => {
            const isActive = pathname === link.to || (link.to !== '/' && pathname.startsWith(link.to));
            return (
              <Link
                key={link.to}
                to={link.to}
                className={`flex items-center gap-2 px-3 py-2 rounded-lg font-semibold text-sm transition-all duration-200 hover:-translate-y-0.5 hover:shadow-lg active:scale-95 ${
                  isActive
                    ? 'text-primary font-bold border-b-2 border-primary bg-primary/10'
                    : 'text-zinc-600 hover:text-zinc-900 hover:bg-zinc-100/80 hover:shadow-zinc-200/50'
                }`}
              >
                <span className="material-symbols-outlined text-[18px]">{link.icon}</span>
                {link.label}
              </Link>
            );
          })}
        </div>
      </div>

      <Link 
        to="/admin" 
        className="text-zinc-600 hover:text-primary transition-colors p-2 rounded-full hover:bg-zinc-100 flex items-center justify-center shrink-0"
        title="Панель управления"
      >
        <span className="material-symbols-outlined text-[24px]">manage_accounts</span>
      </Link>

      {/* Mobile bottom nav */}
      <nav className="bg-white/95 backdrop-blur-md font-sans text-[11px] font-medium fixed bottom-0 w-full z-50 border-t border-zinc-200 shadow-xl left-0 flex justify-around items-center px-1 py-2 md:hidden"
           style={{ paddingBottom: 'max(12px, env(safe-area-inset-bottom))' }}>
        {navLinks.map(link => {
          const isActive = pathname === link.to || (link.to !== '/' && pathname.startsWith(link.to));
          return (
            <Link
              key={link.to}
              to={link.to}
              className={`flex flex-col items-center justify-center px-3 py-1.5 rounded-xl active:scale-90 transition-all duration-150 min-w-[56px] ${
                isActive ? 'text-primary font-bold bg-primary/10' : 'text-zinc-600 hover:text-zinc-900 hover:bg-zinc-100/50'
              }`}
            >
              <span className="material-symbols-outlined text-[22px]" style={isActive ? {fontVariationSettings:"'FILL' 1"} : {}}>{link.icon}</span>
              <span className="mt-0.5 leading-tight">{link.label}</span>
            </Link>
          );
        })}
      </nav>
    </nav>
  );
}
