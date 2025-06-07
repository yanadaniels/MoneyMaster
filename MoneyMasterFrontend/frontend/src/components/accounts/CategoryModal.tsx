import Modal from "@components/Modal";
import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faFilter, faTrash } from "@fortawesome/free-solid-svg-icons";
import { CategoryResponse } from "@/types";
import { categoryService } from "@/services/categoryService";

interface CategoryModalProps {
    isOpen: boolean;
    onClose: () => void;
}

const CategoryModal: React.FC<CategoryModalProps> = ({ isOpen, onClose }) => {
    const [categories, setCategories] = useState<CategoryResponse[]>([]);

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const data = await categoryService.getCategories();
                setCategories(data);
            } catch (error) {
                console.error("Ошибка при загрузке категорий:", error);
            }
        };

        fetchCategories();
    }, []);

    const renderCategoriesByType = (categoryType: string) => {
        const filteredCategories = categories.filter(category => category.categoryType === categoryType);

        return (
            <ul>
                {filteredCategories && filteredCategories.length > 0 ? (
                    filteredCategories.map((category, index) => (
                        <li key={index}>
                            {category.name}
                            <FontAwesomeIcon
                                icon={faTrash}
                                className="text-2xl text-gray-700 transition-transform duration-300 hover:scale-120"
                            />
                        </li>
                    ))
                ) : (
                    <li>У вас нет категорий </li>
                )}
            </ul>
        );
    };

    return (
        <Modal isOpen={isOpen} onClose={onClose}>
            <div>
                <h3 className="p-4 pb-2 text-lg font-medium border-1 border-white border-b-gray-200 mb-2 flex items-center justify-between">
                    Мои категории
                </h3>
                <div className="mt-4 pb-4 bg-white rounded-md shadow-md overflow-hidden">
                    <h2 className="p-4 pb-2 text-lg font-medium border-1 border-white border-b-gray-200 mb-2 flex items-center justify-between">
                        Категории пополнения
                        <FontAwesomeIcon
                            icon={faFilter}
                            className="text-2xl text-gray-700 transition-transform duration-300 hover:scale-120"
                        />
                    </h2>
                    {renderCategoriesByType('Revenue')}
                </div>
                <div className="mt-4 pb-4 bg-white rounded-md shadow-md overflow-hidden">
                    <h2 className="p-4 pb-2 text-lg font-medium border-1 border-white border-b-gray-200 mb-2 flex items-center justify-between">
                        Категории трат
                        <FontAwesomeIcon
                            icon={faFilter}
                            className="text-2xl text-gray-700 transition-transform duration-300 hover:scale-120"
                        />
                    </h2>
                    {renderCategoriesByType('Expenses')}
                </div>
            </div>
        </Modal>
    );
};

export default CategoryModal;