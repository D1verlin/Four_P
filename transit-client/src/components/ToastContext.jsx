import { createContext, useContext, useState, useCallback } from 'react';

const ToastContext = createContext(null);

export function ToastProvider({ children }) {
  const [toast, setToast] = useState(null);

  const showToast = useCallback((message, type = 'info') => {
    setToast({ message, type });
    setTimeout(() => {
      setToast(null);
    }, 3000);
  }, []);

  return (
    <ToastContext.Provider value={{ showToast }}>
      {children}
      {toast && (
        <div className="fixed bottom-24 left-1/2 -translate-x-1/2 z-[100] animate-slide-up">
          <div className="bg-surface-container-highest text-on-surface px-4 py-3 rounded-xl shadow-lg border border-outline-variant/30 flex items-center gap-3 min-w-[200px]">
            <span className="material-symbols-outlined text-primary">
              {toast.type === 'success' ? 'check_circle' : 'info'}
            </span>
            <span className="font-semibold text-sm">{toast.message}</span>
          </div>
        </div>
      )}
    </ToastContext.Provider>
  );
}

export const useToast = () => useContext(ToastContext);
