import React from 'react';

interface IconProps {
    Icon: React.FC<React.SVGProps<SVGSVGElement>>; //  омпонент SVG
    color?: string;    // ÷вет иконки (по умолчанию Ч черный)
    size?: string;     // –азмер иконки (по умолчанию Ч 6)
}

const SvgIcon: React.FC<IconProps> = ({ Icon, color = 'currentColor', size = '6' }) => {
    return (
        <Icon
            className={`w-${size} h-${size} text-${color}`} // ”станавливаем размеры и цвет
            fill="currentColor"
        />
    );
};

export default SvgIcon;
