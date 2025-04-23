// src/components/IconRenderer.tsx
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { iconMap } from "@/constants/iconList";

interface IconRendererProps {
  iconName?: string;
  className?: string;
}

const IconRenderer: React.FC<IconRendererProps> = ({ iconName, className }) => {
  const icon = iconName && iconMap[iconName];

  return icon ? (
    <FontAwesomeIcon icon={icon} className={className} />
  ) : (
    <span className="text-gray-400">---</span>
  );
};

export default IconRenderer;
