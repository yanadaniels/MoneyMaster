import { useState } from "react";
import { categoryService } from "@/services/categoryService"; // Предполагается, что у вас есть такой сервис
import { CategoryResponse } from "@/types";
import { useCategoryContext } from "@/Context/CategoryContext";

interface AddCategoryFormProps {
  categoryType: string,
  onCategoryAdded: (newCategory: CategoryResponse | null) => void;
}


const AddCategory: React.FC<AddCategoryFormProps> = ({categoryType, onCategoryAdded }) => {
  const [categoryName, setCategoryName] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const { addCategory } = useCategoryContext();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!categoryName.trim()) {
      setError("Название категории не может быть пустым.");
      return;
    }

    setIsSubmitting(true);
    setError(null);

    try {
      const newCategory = await categoryService.createCategory(categoryName,categoryType);
      
      if (newCategory) {
        onCategoryAdded(newCategory);
        addCategory(newCategory);
      }
      
      setCategoryName("");
    } catch (err) {
      console.error("Ошибка при создании категории:", err);
      setError("Не удалось создать категорию. Пожалуйста, попробуйте снова.");
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <input
          id="categoryName"
          type="text"
          value={categoryName}
          onChange={(e) => setCategoryName(e.target.value)}
          className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Введите название новой категории"
          disabled={isSubmitting}
        />
        {error && <p className="text-red-500 text-xs mt-1">{error}</p>}
      </div>
      <div className="flex justify-end">
        <button
          type="submit"
          disabled={isSubmitting || !categoryName.trim()}
          className={`px-4 py-2 rounded-lg text-white ${
            isSubmitting || !categoryName.trim()
              ? "bg-blue-300 cursor-not-allowed"
              : "bg-blue-500 hover:bg-blue-600"
          }`}
        >
          {isSubmitting ? "Создание..." : "Создать категорию"}
        </button>
      </div>
    </form>
  );
};

export default AddCategory;