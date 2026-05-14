export default function Footer() {
  return (
    <footer className="w-full py-8 px-6 flex flex-col md:flex-row justify-between items-center gap-4 mt-auto bg-zinc-950 border-t border-zinc-800 font-['Plus_Jakarta_Sans'] text-sm pb-20 md:pb-8">
      <div className="flex items-center gap-6">
        <span className="text-lg font-bold text-zinc-200">UrbanTransit</span>
        <span className="text-zinc-500 hidden md:inline">© 2024 Urban Transit System. All rights reserved.</span>
      </div>
      <div className="flex flex-wrap items-center justify-center gap-6">
        <a className="text-zinc-500 hover:text-white transition-colors cursor-pointer" href="#">Политика конфиденциальности</a>
        <a className="text-zinc-500 hover:text-white transition-colors cursor-pointer" href="#">Условия использования</a>
        <a className="text-zinc-500 hover:text-white transition-colors cursor-pointer" href="#">Помощь</a>
        <a className="text-zinc-500 hover:text-white transition-colors cursor-pointer" href="#">Контакты</a>
      </div>
      <div className="text-zinc-500 md:hidden w-full text-center mt-4">
        © 2024 Urban Transit System. All rights reserved.
      </div>
    </footer>
  );
}
