// utils/storage.ts
export const storage = {
  get: <T>(key: string): T | null => {
    try {
      const item = localStorage.getItem(key);
      return item ? JSON.parse(item) : null;
    } catch (e) {
      console.error(`Error getting ${key} from localStorage`, e);
      return null;
    }
  },
  
  set: (key: string, value: any): void => {
    try {
      localStorage.setItem(key, JSON.stringify(value));
    } catch (e) {
      console.error(`Error setting ${key} in localStorage`, e);
    }
  },
  
  remove: (key: string): void => {
    localStorage.removeItem(key);
  }
};