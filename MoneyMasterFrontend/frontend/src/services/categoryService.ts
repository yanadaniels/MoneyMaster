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

    createCategory: async (
        name: string,
        categoryType: string
    ) => {
        try {
            const accessToken = authService.getToken();
            if (!accessToken) return;

            const response = await api.post(
                "/categories",
                {
                    name,
                    categoryType
                },
                { headers: { Authorization: `Bearer ${accessToken}` } }
            );
            return response.data;
        } catch (error) {
            console.error("Ошибка при создани категории!:", error);
            throw error;
        }
    },

  //createCategory: async (
  //  userId: string | undefined,
  //  name: string,
  //  balance: number,
  //  accountTypeId: string | undefined,
  //  currency: string
  //) => {
  //  try {
  //    const accessToken = authService.getToken();
  //    if (!accessToken) return;

  //    console.log({ userId, name, balance, accountTypeId, icon: "", currency });
  //    const response = await api.post(
  //      "/categories",
  //      {
  //        userId,
  //        name,
  //        balance,
  //        accountTypeId,
  //        icon: "string",
  //        currency,
  //        createAt: "2025-03-04T12:43:03.438Z",
  //      },
  //      { headers: { Authorization: `Bearer ${accessToken}` } }
  //    );
  //    return response.data;
  //  } catch (error) {
  //    console.error("Ошибка при создани счета:", error);
  //    throw error;
  //  }
  //},

  deleteCategory: async (accountId: string) => {
    const accessToken = authService.getToken();
    if (!accessToken) return;

    await api.delete(`/categories/${accountId}`, {
      headers: { Authorization: `Bearer ${accessToken}` },
    });
  },
};
