import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import authService from "../services/authService";
import { useAuth } from "../Context/AuthContext";

const LoginPage: React.FC = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();
  const { dispatch } = useAuth();

  const handleLogin = async () => {
    try {
      await authService.login(email, password, dispatch);
      navigate("/");
    } catch (error) {
      alert("Неверный логин или пароль. Пожалуйста попробуйте снова");
    }
  };

  return (
    <div
      className="flex text-3x1 min-h-full flex-col justify-center px-6 py-12 lg:px-8 before:absolute before:w-250
                before:h-250 before:bg-no-repeat before:bg-[url('/ruble-svg.svg')] before:opacity-4 before:bg-contain 
                before:absolute before:left-0 before:top-10 before:z-0 before:-translate-x-50"
    >
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
        <form className="space-y-6" action="#" method="POST">
          <div>
            <label htmlFor="login" className="block font-medium text-gray-900">
              Логин
            </label>
            <div className="mt-2">
              <input
                type="text"
                name="login"
                id="login"
                required
                className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 
                           -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2
                           focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
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
            <div className="mt-2">
              <input
                type="password"
                name="password"
                id="password"
                required
                className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 
                                           -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2
                                           focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>
          </div>

          <div>
            <button
              type="button"
              className="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 
                                       text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2
                                       focus-visible:outline-indigo-600"
              onClick={handleLogin}
            >
              Войти
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
