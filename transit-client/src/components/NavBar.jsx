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
    <nav className="sticky top-0 z-50 flex justify-between items-center px-4 md:px-6 py-3 w-full bg-zinc-950/80 backdrop-blur-md border-b border-zinc-800 font-sans antialiased">
      <div className="flex items-center gap-4 md:gap-6">
        <Link to="/" className="text-lg md:text-xl font-bold tracking-tight text-white flex items-center gap-2 shrink-0">
          <span className="material-symbols-outlined text-orange-500" style={{fontVariationSettings:"'FILL' 1"}}>directions_transit</span>
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
                    ? 'text-orange-400 font-bold border-b-2 border-orange-500 bg-orange-500/10'
                    : 'text-zinc-400 hover:text-zinc-100 hover:bg-zinc-800/80 hover:shadow-zinc-900/50'
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
        className="text-zinc-400 hover:text-orange-400 transition-colors p-2 rounded-full hover:bg-zinc-800 flex items-center justify-center shrink-0"
        title="Панель управления"
      >
        <span className="material-symbols-outlined text-[24px]">manage_accounts</span>
      </Link>

      {/* Mobile bottom nav */}
      <nav className="bg-zinc-900/95 backdrop-blur-md font-sans text-[11px] font-medium fixed bottom-0 w-full z-50 border-t border-zinc-800 shadow-2xl left-0 flex justify-around items-center px-1 py-2 md:hidden"
           style={{ paddingBottom: 'max(12px, env(safe-area-inset-bottom))' }}>
        {navLinks.map(link => {
          const isActive = pathname === link.to || (link.to !== '/' && pathname.startsWith(link.to));
          return (
            <Link
              key={link.to}
              to={link.to}
              className={`flex flex-col items-center justify-center px-3 py-1.5 rounded-xl active:scale-90 transition-all duration-150 min-w-[56px] ${
                isActive ? 'text-orange-400 font-bold bg-orange-500/10' : 'text-zinc-500 hover:text-zinc-200 hover:bg-zinc-800/50'
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
