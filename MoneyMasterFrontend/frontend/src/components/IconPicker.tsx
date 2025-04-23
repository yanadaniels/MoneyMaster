// src/components/IconPicker.tsx
import { iconMap, iconOptions } from "@/constants/iconList";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

interface IconPickerProps {
  selectedIcon: string;
  onSelect: (icon: string) => void;
}

const IconPicker: React.FC<IconPickerProps> = ({ selectedIcon, onSelect }) => {
  return (
    <div className="grid grid-cols-6 gap-4 mt-2">
      {iconOptions.map((iconName) => (
        <button
          key={iconName}
          type="button"
          onClick={() => onSelect(iconName)}
          className={`p-3 border rounded-xl text-xl ${
            selectedIcon === iconName
              ? "border-blue-500 bg-blue-100"
              : "border-gray-300 hover:bg-gray-50"
          }`}
        >
          <FontAwesomeIcon icon={iconMap[iconName]} />
        </button>
      ))}
    </div>
  );
};

export default IconPicker;
