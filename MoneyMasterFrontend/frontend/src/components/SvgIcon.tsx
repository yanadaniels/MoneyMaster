import React from 'react';

interface IconProps {
    Icon: React.FC<React.SVGProps<SVGSVGElement>>; // ��������� SVG
    color?: string;    // ���� ������ (�� ��������� � ������)
    size?: string;     // ������ ������ (�� ��������� � 6)
}

const SvgIcon: React.FC<IconProps> = ({ Icon, color = 'currentColor', size = '6' }) => {
    return (
        <Icon
            className={`w-${size} h-${size} text-${color}`} // ������������� ������� � ����
            fill="currentColor"
        />
    );
};

export default SvgIcon;
