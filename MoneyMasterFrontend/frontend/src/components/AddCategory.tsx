import { useState } from "react";

interface AddCategoryFormProps {
  onAddCategory: (categoryName: string) => void;
}

const AddCategory: React.FC<AddCategoryFormProps> = ({ onAddCategory }) => {
  const [categoryName, setCategoryName] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!categoryName.trim()) {
      setError("Название категории не может быть пустым.");
      return;
    }
    setIsSubmitting(true);
    // Здесь вы можете добавить логику для отправки данных на сервер
    setTimeout(() => {
      onAddCategory(categoryName); // Вызываем функцию для добавления категории
      setCategoryName(""); // Очищаем поле ввода
      setIsSubmitting(false);
    }, 500); // Эмуляция задержки при добавлении
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <input
          id="categoryName"
          type="text"
          value={categoryName}
          onChange={(e) => setCategoryName(e.target.value)}
          className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Введите название новой категории"
        />
        {error && <p className="text-red-500 text-xs mt-1">{error}</p>}
      </div>
    </form>
  );
};

export default AddCategory;
``;
