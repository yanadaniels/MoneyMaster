import { createContext, useContext, useReducer, ReactNode, useEffect } from "react";
import { authService } from "@/services/authService";
import { User } from "@/types/user";
import { storage } from "@/utils/storage";


interface AuthState {
  user: User | null;
  isLoading: boolean; // Добавляем состояние загрузки
}

type AuthAction = 
  | { type: "LOGIN"; payload: User } 
  | { type: "LOGOUT" }
  | { type: "LOADING"; payload: boolean };

const initialState: AuthState = {
  user: null,
  isLoading: true, // Начальное состояние загрузки
};

const authReducer = (state: AuthState, action: AuthAction) => {
  switch (action.type) {
    case "LOGIN":
      return { ...state, user: action.payload, isLoading: false };
    case "LOGOUT":
      return { ...state, user: null, isLoading: false };
    case "LOADING":
      return { ...state, isLoading: action.payload };
    default:
      return state;
  }
};

const AuthContext = createContext<
  { state: AuthState; dispatch: React.Dispatch<AuthAction> } | undefined
>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [state, dispatch] = useReducer(authReducer, initialState);

  useEffect(() => {
    const checkAuth = async () => {
      try {
        dispatch({ type: "LOADING", payload: true });
        const token = localStorage.getItem('accsessToken');
        const userId = storage.get<string>('userId');
      
        if (token && userId) {
          try {
            const user = await authService.getUserById(userId);
            if (user) dispatch({ type: "LOGIN", payload: user });
            
            storage.set('userId', user?.id);
          } catch (error) {
            console.error("Failed to fetch user:", error);
            const storedUser = localStorage.getItem('user');
            if (storedUser) {
              dispatch({ type: "LOGIN", payload: JSON.parse(storedUser) });
            } else {
              clearAuthData();
            }
          }
        }
      } catch (error) {
        console.error("Auth check failed:", error);
        clearAuthData();
      } finally {
        dispatch({ type: "LOADING", payload: false });
      }
    };

    checkAuth();
  }, []);

  const clearAuthData = () => {
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('user');
  };

  return (
    <AuthContext.Provider value={{ state, dispatch }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};