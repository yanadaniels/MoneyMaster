import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { authService } from "@/services/authService";
import { useAuth } from "../Context/AuthContext";
import { FaEye, FaEyeSlash } from "react-icons/fa";

const LoginPage: React.FC = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();
  const { dispatch } = useAuth();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setIsLoading(true);

    try {
      await authService.login(email, password, dispatch);
      navigate("/accounts");
    } catch (error: any) {
      if (error.response) {
        // Обработка различных HTTP статусов
        switch (error.response.status) {
          case 400:
            setError("Неверный формат данных");
            break;
          case 401:
            setError("Неверный email или пароль");
            break;
          case 403:
            setError("Доступ запрещен");
            break;
          case 500:
            setError("Ошибка сервера. Пожалуйста, попробуйте позже");
            break;
          default:
            setError("Произошла ошибка. Пожалуйста, попробуйте снова");
        }
      } else {
        setError("Сервер не отвечает. Проверьте подключение к интернету");
      }
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="flex text-3x1 min-h-full flex-col justify-center px-6 py-12 lg:px-8 before:absolute before:w-250 before:h-250 before:bg-no-repeat before:bg-[url('/ruble-svg.svg')] before:opacity-4 before:bg-contain before:absolute before:left-0 before:top-10 before:z-0 before:-translate-x-50">
      <div className="sm:mx-auto sm:w-full sm:max-w-sm">
        <img
          className="mx-auto h-10 w-auto"
          src="https://tailwindui.com/plus-assets/img/logos/mark.svg?color=indigo&shade=600"
          alt="Your Company"
        />
        <h2 className="mt-10 text-center text-2xl/9 font-bold tracking-tight text-gray-900">
          Вход в аккаунт
        </h2>
      </div>

      <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm relative z-1">
        {/* Блок для отображения ошибок */}
        {error && (
          <div className="mb-4 p-3 bg-red-100 text-red-700 rounded-md text-sm">
            {error}
          </div>
        )}

        <form className="space-y-6" onSubmit={handleLogin}>
          <div>
            <label htmlFor="email" className="block font-medium text-gray-900">
              Email
            </label>
            <div className="mt-2">
              <input
                type="email"
                name="email"
                id="email"
                required
                className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>
          </div>

          <div>
            <div className="flex items-center justify-between">
              <label
                htmlFor="password"
                className="block font-medium text-gray-900"
              >
                Пароль
              </label>
              <div className="text-sm">
                <a
                  href="#"
                  className="font-semibold text-indigo-600 hover:text-indigo-500"
                >
                  Забыли пароль?
                </a>
              </div>
            </div>
            <div className="mt-2 relative">
              <input
                type={showPassword ? "text" : "password"}
                name="password"
                id="password"
                required
                className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
              <button
                type="button"
                className="absolute inset-y-0 right-0 pr-3 flex items-center text-gray-400 hover:text-gray-600"
                onClick={() => setShowPassword(!showPassword)}
              >
                {showPassword ? <FaEyeSlash /> : <FaEye />}
              </button>
            </div>
          </div>

          <div>
            <button
              type="submit"
              disabled={isLoading}
              className={`flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600 ${
                isLoading ? "opacity-50 cursor-not-allowed" : ""
              }`}
            >
              {isLoading ? "Вход..." : "Войти"}
            </button>
          </div>
        </form>

        <p className="mt-10 text-center text-gray-500">
          Нет аккаунта?
          <Link
            to="/registration"
            className="font-semibold text-indigo-600 hover:text-indigo-500"
          >
            {" "}
            Регистрация
          </Link>
        </p>
      </div>
    </div>
  );
};

export default LoginPage;