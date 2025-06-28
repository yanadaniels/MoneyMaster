import { Outlet, Navigate } from 'react-router-dom';
import { authService } from "@/services/authService";

const PrivateRoute = () => {
  const isAuth = authService.getToken() // Проверяем JWT
  return isAuth ? <Outlet /> : <Navigate to="/login" replace />;
};

export default PrivateRoute;