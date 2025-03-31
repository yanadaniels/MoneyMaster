import { Routes, Route } from "react-router-dom";
import LoginPage from "../pages/LoginPage";
import HomePage from "../pages/HomePage";
import PrivateRoute from "./PrivateRoute";
import RegistrationPage from "../pages/RegistrationPage";

function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<PrivateRoute element={<HomePage />} />} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/registration" element={<RegistrationPage />} />
    </Routes>
  );
}

export default AppRoutes;
