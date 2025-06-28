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
    const [showModalExpenses, setShowModalExpenses] = useState(false);
    const [showModalRevenue, setShowModalRevenue] = useState(false);
    const [revenueСategoryName, setRevenueCategoryName] = useState('');


    const openModalExpenses = () => {
        setShowModalExpenses(true);
    }
    const closeModalExpenses = () => {
        setShowModalExpenses(false);
    }

    const openModalRevenue = () => {
        setShowModalRevenue(true);
    }
    const closeModalRevenue = () => {
        setShowModalRevenue(false);
        setRevenueCategoryName("");
    }

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

    const addRevenueCategory = async (categoryName: string) => {
        await categoryService.createCategory(categoryName, "Revenue");

        const updatedData = await categoryService.getCategories();
        setCategories(updatedData);
    };

    const deleteCategory = async (categoryId: string) => {
        await categoryService.deleteCategory(categoryId);

        const updatedData = await categoryService.getCategories();
        setCategories(updatedData);
    }


    const renderCategoriesByType = (categoryType: string) => {
        const filteredCategories = categories.filter(category => category.categoryType === categoryType);


        return (
            <ul>
                {filteredCategories && filteredCategories.length > 0 ? (
                    filteredCategories.map((category, index) => (
                        <li key={index}>
                            <div className="d-flex">
                                {category.name}
                                <button className="icon-button" onClick={() => deleteCategory(category.id)}>
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

    return (
        <Modal isOpen={isOpen} onClose={onClose}>
            <div className="modal-content">
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
                    <button onClick={openModalRevenue} className="bg-green-500 text-white px-4 py-2 rounded">добавить категорию пополнения</button>
                </div>
                <div>
                    {showModalRevenue && (
                        <div className="modal-container">
                            <div className="input-container">
                                <input
                                    className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 outline-gray-300 placeholder:text-gray-400 focus:outline-indigo-600"
                                    type="text"
                                    placeholder="Введите текст"
                                    value={revenueСategoryName}
                                    onChange={(e) => setRevenueCategoryName(e.target.value)}
                                />
                            </div>
                            <div className="cancel-button">
                                <span className="bg-gray-400 text-white px-4 py-2 rounded"
                                    onClick={closeModalRevenue}>Отмена
                                </span>
                                <span className="bg-green-500 text-white px-4 py-2 rounded"
                                    onClick={() => addRevenueCategory(revenueСategoryName)}>Добавить
                                </span>
                            </div>
                        </div>
                    )}
                </div>
                <div className="mt-4 pb-4 bg-white rounded-md shadow-md overflow-hidden">
                    <h2 className="p-4 pb-2 text-lg font-medium border-1 border-white border-b-gray-200 mb-2 flex items-center justify-between">
                        Категории трат
                    </h2>
                    {renderCategoriesByType('Expenses')}
                </div>
                <div className="d-flex-category">
                    <button onClick={openModalExpenses} className="bg-green-500 text-white px-4 py-2 rounded">добавить категорию трат</button>
                </div>
                <div>
                    {showModalExpenses && (
                        <div className="modal-container">
                            <div className="input-container">
                                <input className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 outline-gray-300 placeholder:text-gray-400 focus:outline-indigo-600"
                                    type="text"
                                    placeholder="Введите текст" />
                            </div>
                            <div className="cancel-button">
                                <span className="bg-gray-400 text-white px-4 py-2 rounded" onClick={closeModalExpenses}>Отмена</span>
                                <span className="bg-green-500 text-white px-4 py-2 rounded" onClick={closeModalExpenses}>Добавить</span>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </Modal>
    );
};

export default CategoryModal;