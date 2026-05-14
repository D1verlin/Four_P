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
    <nav className="sticky top-0 z-50 flex justify-between items-center px-6 py-3 w-full bg-zinc-950/80 backdrop-blur-md border-b border-zinc-800 font-['Plus_Jakarta_Sans'] antialiased">
      <div className="flex items-center gap-6">
        <Link to="/" className="text-xl font-bold tracking-tight text-white flex items-center gap-2">
          <span className="material-symbols-outlined text-orange-500" style={{fontVariationSettings:"'FILL' 1"}}>directions_transit</span>
          UrbanTransit
        </Link>
        <div className="hidden md:flex gap-1">
          {navLinks.map(link => {
            const isActive = pathname === link.to || (link.to !== '/' && pathname.startsWith(link.to));
            return (
              <Link
                key={link.to}
                to={link.to}
                className={`flex items-center gap-2 px-3 py-2 rounded-md font-label-lg text-label-lg transition-all active:scale-95 ${
                  isActive
                    ? 'text-orange-500 font-bold border-b-2 border-orange-500'
                    : 'text-zinc-400 hover:text-zinc-200 hover:bg-zinc-900'
                }`}
              >
                <span className="material-symbols-outlined text-[18px]">{link.icon}</span>
                {link.label}
              </Link>
            );
          })}
        </div>
      </div>
      <div className="flex items-center gap-2">
        <button className="text-orange-500 hover:bg-zinc-900 rounded-md transition-all active:scale-95 p-2 flex items-center justify-center">
          <span className="material-symbols-outlined">account_circle</span>
        </button>
      </div>

      {}
      <nav className="bg-zinc-900/95 backdrop-blur-md font-['Plus_Jakarta_Sans'] text-[11px] font-medium fixed bottom-0 w-full z-50 border-t border-zinc-800 shadow-2xl left-0 flex justify-around items-center px-2 py-3 md:hidden">
        {navLinks.map(link => {
          const isActive = pathname === link.to || (link.to !== '/' && pathname.startsWith(link.to));
          return (
            <Link
              key={link.to}
              to={link.to}
              className={`flex flex-col items-center justify-center px-3 py-1 rounded-xl active:scale-90 transition-all ${
                isActive ? 'text-orange-500 font-bold bg-orange-600/10' : 'text-zinc-500 hover:text-zinc-200'
              }`}
            >
              <span className="material-symbols-outlined" style={isActive ? {fontVariationSettings:"'FILL' 1"} : {}}>{link.icon}</span>
              <span className="mt-1">{link.label}</span>
            </Link>
          );
        })}
      </nav>
    </nav>
  );
}
