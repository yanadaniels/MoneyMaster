import Modal from "@components/Modal";
import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrash } from "@fortawesome/free-solid-svg-icons";
import { CategoryResponse } from "@/types";
import { categoryService } from "@/services/categoryService";
import './CategoryModal.css';

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

        function press() { console.log("Button clicked!"); }

        return (
            <ul>
                {filteredCategories && filteredCategories.length > 0 ? (
                    filteredCategories.map((category, index) => (
                        <li key={index}>
                            <div className="d-flex">
                                {category.name}
                                <button className="icon-button" onClick={press}>
                                    <FontAwesomeIcon
                                        icon={faTrash}
                                        className="text-2xl text-gray-700 transition-transform duration-300 hover:scale-120"
                                    />
                                </button>
                            </div>
                        </li>
                    ))
                ) : (
                    <li>У вас нет категорий </li>
                )}
            </ul>
        );
    };

    const handleAddExpenseCategory = () => {
        console.log("Кнопка 'добавить категорию трат' нажата");
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
                    </h2>
                    {renderCategoriesByType('Revenue')}
                </div>
                <div className="d-flex-category">
                    <button className="bg-green-500 text-white px-4 py-2 rounded">добавить категорию пополнения</button>
                </div>
                <div className="mt-4 pb-4 bg-white rounded-md shadow-md overflow-hidden">
                    <h2 className="p-4 pb-2 text-lg font-medium border-1 border-white border-b-gray-200 mb-2 flex items-center justify-between">
                        Категории трат
                    </h2>
                    {renderCategoriesByType('Expenses')}
                </div>
                <div className="d-flex-category">
                    <button onClick={handleAddExpenseCategory} className="bg-green-500 text-white px-4 py-2 rounded">добавить категорию трат</button>
                </div>
            </div>
        </Modal>
    );
};

export default CategoryModal;