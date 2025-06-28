import React, { createContext, useContext, useState, useEffect, ReactNode, useCallback } from "react";
import { CategoryResponse } from "@/types";
import { categoryService } from "@/services/categoryService";

// Выносим тип контекста в отдельный интерфейс
export interface ICategoryContext {
  categories: CategoryResponse[];
  loading: boolean;
  error: string | null;
  addCategory: (category: CategoryResponse) => void;
  updateCategory: (updatedCategory: CategoryResponse) => void;
  deleteCategory: (id: string) => void;
  fetchCategories: () => Promise<void>;
  getCategoryById: (id: string) => CategoryResponse | undefined;
}

// Создаем контекст с явным типом
const CategoryContext = createContext<ICategoryContext | null>(null);

// Добавляем displayName для лучшей отладки
CategoryContext.displayName = "CategoryContext";

// Кастомный хук для использования контекста
export const useCategoryContext = () => {
  const context = useContext(CategoryContext);
  if (!context) {
    throw new Error("useCategoryContext must be used within a CategoryProvider");
  }
  return context;
};

// Интерфейс для пропсов провайдера
interface ICategoryProviderProps {
  children: ReactNode;
  initialCategories?: CategoryResponse[]; // Опциональные начальные категории
}

// Экспортируем провайдер как именованный экспорт
export const CategoryProvider: React.FC<ICategoryProviderProps> = ({ 
  children, 
  initialCategories = [] 
}) => {
  const [categories, setCategories] = useState<CategoryResponse[]>(initialCategories);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  // Метод для загрузки категорий
  const fetchCategories = useCallback(async () => {
    try {
      setLoading(true);
      setError(null);
      const response = await categoryService.getCategories();
      setCategories(response);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to fetch categories");
      console.error("Failed to fetch categories:", err);
    } finally {
      setLoading(false);
    }
  }, []);

  // Добавление категории
  const addCategory = useCallback((category: CategoryResponse) => {
    setCategories(prev => [...prev, category]);
  }, []);

  // Обновление категории
  const updateCategory = useCallback((updatedCategory: CategoryResponse) => {
    setCategories(prev =>
      prev.map(category =>
        category.id === updatedCategory.id ? updatedCategory : category
      )
    );
  }, []);

  // Удаление категории
  const deleteCategory = useCallback((id: string) => {
    setCategories(prev => prev.filter(category => category.id !== id));
  }, []);

  // Получение категории по ID
  const getCategoryById = useCallback(
    (id: string) => categories.find(category => category.id === id),
    [categories]
  );

  // Загружаем категории при монтировании
  useEffect(() => {
    fetchCategories();
  }, [fetchCategories]);

  // Значение контекста
  const contextValue = React.useMemo(
    () => ({
      categories,
      loading,
      error,
      addCategory,
      updateCategory,
      deleteCategory,
      fetchCategories,
      getCategoryById,
    }),
    [categories, loading, error, addCategory, updateCategory, deleteCategory, fetchCategories, getCategoryById]
  );

  return (
    <CategoryContext.Provider value={contextValue}>
      {children}
    </CategoryContext.Provider>
  );
};