import { BrowserRouter as Router, Routes, Route, Navigate, Outlet } from "react-router-dom";
import LoginPage from "../pages/LoginPage";
import HomePage from "../pages/HomePage";
import PrivateRoute from "./PrivateRoute";
import RegistrationPage from "../pages/RegistrationPage";

// Компонент для перенаправления аутентифицированных пользователей
const PublicRoute = () => {
  const isAuth = localStorage.getItem('jwtToken');
  return isAuth ? <Navigate to="/accounts" replace /> : <Outlet />;
};

function AppRoutes() {
  return (
    <Routes>
      {/* Публичные маршруты (только для НЕаутентифицированных) */}
      <Route element={<PublicRoute />}>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/registration" element={<RegistrationPage />} />
      </Route>

      {/* Защищённые маршруты (только для аутентифицированных) */}
      <Route element={<PrivateRoute />}>
        <Route path="/accounts" element={<HomePage />} />
      </Route>

      {/* Дефолтный маршрут (404) */}
      <Route path="*" element={<div>404</div>} />
    </Routes>
  );
}

export default AppRoutes;