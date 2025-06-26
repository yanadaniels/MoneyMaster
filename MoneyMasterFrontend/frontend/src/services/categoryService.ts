import { CategoryCreate, CategoryResponse } from "@/types";
import api from "../api/api";
import { authService } from "@/services/authService";

export const categoryService = {
  getCategories: async () => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return;

      const response = await api.get("/categories", {
        headers: { Authorization: `Bearer ${accessToken}` },
      });
      return response.data;
    } catch (error) {
      console.error("Ошибка при получении счетов:", error);
      throw error;
    }
  },

  createCategory: async ( newCategory: CategoryCreate): Promise<CategoryResponse | null> => {
    try {
      const accessToken = authService.getToken();
      if (!accessToken) return null;

      console.log(newCategory.name, newCategory.icon, newCategory.categoryType );
      const response = await api.post(
        "/categories",
        newCategory,
        { headers: { Authorization: `Bearer ${accessToken}` } }
      );
      return response.data;
    } catch (error) {
      console.error("Ошибка при создани категории:", error);
      throw error;
    }
  },

  deleteCategory: async (accountId: string) => {
    const accessToken = authService.getToken();
    if (!accessToken) return;

    await api.delete(`/categories/${accountId}`, {
      headers: { Authorization: `Bearer ${accessToken}` },
    });
  },
};
