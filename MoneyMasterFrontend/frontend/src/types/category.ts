export interface CategoryResponse {
  id: string;
  name: string;
  icon: string;
  categoryType: string;
  isSystem: boolean;
  isDeleted: boolean;
  createAt: string;
}

export interface CategoryCreate {
  name: string;
  icon: string;
  categoryType: string;
}
