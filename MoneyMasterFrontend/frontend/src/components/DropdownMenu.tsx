import React, { useState, useRef, useEffect } from "react";

interface DropdownMenuProps {
  children: React.ReactNode;
  items: { label: string; onClick: () => void }[];
}

const DropdownMenu: React.FC<DropdownMenuProps> = ({ children, items }) => {
  const [isOpen, setIsOpen] = useState(false);
  const menuRef = useRef<HTMLDivElement>(null);
  const timeoutRef = useRef<ReturnType<typeof setTimeout> | null>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
        setIsOpen(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  return (
    <div
      className="relative"
      ref={menuRef}
      onMouseEnter={() => {
        if (timeoutRef.current) clearTimeout(timeoutRef.current);
        setIsOpen(true);
      }}
      onMouseLeave={() =>
        (timeoutRef.current = setTimeout(() => setIsOpen(false), 200))
      }
    >
      <button className="flex items-center" onClick={() => setIsOpen(!isOpen)}>
        {children}
      </button>
      {isOpen && (
        <div className="absolute right-0 mt-2 w-48 bg-white border rounded-lg shadow-lg">
          {items.map((item, index) => (
            <button
              key={index}
              onClick={() => {
                item.onClick();
                setIsOpen(false);
              }}
              className="block w-full text-left px-4 py-2 text-gray-700 hover:bg-gray-100 cursor-pointer"
            >
              {item.label}
            </button>
          ))}
        </div>
      )}
    </div>
  );
};

export default DropdownMenu;
