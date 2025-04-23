import { ReactNode, useEffect, useState } from "react";
import { X } from "lucide-react";

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  children: ReactNode;
  title?: string;
}

const Modal: React.FC<ModalProps> = ({ isOpen, onClose, children, title }) => {
  const [show, setShow] = useState(false);
  const [isAnimating, setIsAnimating] = useState(false);

  useEffect(() => {
    if (isOpen) {
      // Задержка перед началом анимации появления
      const delayTimer = setTimeout(() => {
        setIsAnimating(true); // Начинаем анимацию
      }, 50); // Задержка 50 мс перед анимацией

      setShow(true);

      return () => clearTimeout(delayTimer); // Очистка таймера при изменении состояния
    } else {
      setIsAnimating(false); // Начинаем анимацию исчезновения
      const timer = setTimeout(() => setShow(false), 300); // Задержка для анимации исчезновения
      return () => clearTimeout(timer);
    }
  }, [isOpen]);

  return (
    <>
      {show && (
        <div className="fixed inset-0 flex items-center justify-center z-50 bg-black/30">
          <div
            className={`bg-white dark:bg-gray-900 rounded-2xl shadow-lg w-full max-w-lg p-6 relative transform transition-all duration-100 ease-in-out ${
              isAnimating
                ? "scale-105 opacity-100" // Когда окно появляется, оно увеличивается и становится видимым
                : "scale-95 opacity-0" // Когда окно исчезает, оно уменьшается и становится невидимым
            }`}
          >
            <button
              onClick={onClose}
              className="absolute top-4 right-4 text-gray-500 hover:text-gray-700 dark:hover:text-gray-300"
            >
              <X size={24} />
            </button>
            {title && <h2 className="text-xl font-semibold mb-4">{title}</h2>}
            <div>{children}</div>
          </div>
        </div>
      )}
    </>
  );
};

export default Modal;
