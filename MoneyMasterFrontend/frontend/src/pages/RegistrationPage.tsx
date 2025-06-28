import React, { useState } from "react";
import { FaEye, FaEyeSlash } from "react-icons/fa";
import { Link, useNavigate } from "react-router-dom";
import { authService } from "@/services/authService";
import { useForm } from "react-hook-form";

type FormData = {
  userName: string;
  email: string;
  password: string;
};

const RegistrationPage: React.FC = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    setError,
  } = useForm<FormData>();

  const [showPassword, setShowPassword] = useState(false);
  const [apiError, setApiError] = useState<string | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const navigate = useNavigate();

  const onSubmit = async (data: FormData) => {
    setIsSubmitting(true);
    setApiError(null); // Сбрасываем предыдущие ошибки
    
    try {
      await authService.registration(data.userName, data.email, data.password);
      navigate("/login");
    } catch (error: any) {
      if (error.response?.status === 409) {
        const errorMessage = error.response.data?.message || "Пользователь с такими данными уже существует";
        setApiError(errorMessage);
        
        if (error.response.data?.field === "email") {
          setError("email", {
            type: "manual",
            message: "Пользователь с таким email уже существует",
          });
        } else if (error.response.data?.field === "userName") {
          setError("userName", {
            type: "manual",
            message: "Это имя пользователя уже занято",
          });
        }
      } else {
        setApiError("Произошла ошибка при регистрации. Пожалуйста, попробуйте снова.");
      }
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8 before:absolute before:w-250 before:h-250 before:bg-no-repeat before:bg-[url('/ruble-svg.svg')] before:opacity-4 before:bg-contain before:absolute before:left-0 before:top-10 before:z-0 before:-translate-x-50">
      <div className="sm:mx-auto sm:w-full sm:max-w-sm">
        <img
          className="mx-auto h-10 w-auto"
          src="https://tailwindui.com/plus-assets/img/logos/mark.svg?color=indigo&shade=600"
          alt="MoneyMaster"
        />
        <h2 className="mt-10 text-center text-2xl/9 font-bold tracking-tight text-gray-900">
          Регистрация
        </h2>
      </div>

      <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm relative z-1">
        <form className="space-y-4" onSubmit={handleSubmit(onSubmit)}>
          {/* Общая ошибка API */}
          {apiError && (
            <div className="mb-4 p-3 bg-red-100 text-red-700 rounded-md">
              {apiError}
            </div>
          )}
      {/* Поле UserName */}
      <div>
        <label
          htmlFor="userName"
          className="block text-sm/6 font-medium text-gray-900"
        >
          Имя пользователя
        </label>
        <div className="mt-2">
          <input
            type="text"
            id="userName"
            autoComplete="off"
            placeholder="Иван"
            className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
            {...register("userName", {
              required: "Имя пользователя не может быть пустым",
              minLength: {
                value: 2,
                message: "Имя пользователя должно быть не менее 2 символов",
              },
              maxLength: {
                value: 16,
                message: "Имя пользователя не должно превышать 16 символов",
              },
            })}
          />
          {errors.userName && (
            <p className="mt-1 text-sm text-red-600">{errors.userName.message}</p>
          )}
        </div>
      </div>

      {/* Поле Email */}
      <div>
        <div className="flex items-center justify-between">
          <label
            htmlFor="email"
            className="block text-sm/6 font-medium text-gray-900"
          >
            Email
          </label>
        </div>
        <div className="mt-2">
          <input
            type="email"
            id="email"
            autoComplete="pochta@mail.ru"
            placeholder="pochta@mail.ru"
            className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
            {...register("email", {
              required: "Требуется адрес электронной почты",
              pattern: {
                value: /^\S+@\S+\.\S+$/,
                message: "Требуется действительный адрес электронной почты",
              },
            })}
          />
          {errors.email && (
            <p className="mt-1 text-sm text-red-600">{errors.email.message}</p>
          )}
        </div>
      </div>

      {/* Поле Password */}
      <div>
        <div className="flex items-center justify-between">
          <label
            htmlFor="password"
            className="block text-sm/6 font-medium text-gray-900"
          >
            Пароль
          </label>
        </div>
        <div className="mt-2 relative">
          <input
            type={showPassword ? "text" : "password"}
            id="password"
            autoComplete="new"
            className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6"
            {...register("password", {
              required: "Парол-passwordь не может быть пустым",
              minLength: {
                value: 8,
                message: "Пароль должен быть не менее 8 символов",
              },
              maxLength: {
                value: 16,
                message: "Пароль не должен превышать 16 символов",
              },
              validate: (value) => {
                const hasUpperCase = /[A-Z]/.test(value);
                const hasLowerCase = /[a-z]/.test(value);
                const hasNumber = /[0-9]/.test(value);
                const hasSpecialChar = /[\!\?\*\.]/.test(value);
                
                if (!hasUpperCase) return "Пароль должен содержать хотя бы одну заглавную букву";
                if (!hasLowerCase) return "Пароль должен содержать хотя бы одну строчную букву";
                if (!hasNumber) return "Пароль должен содержать хотя бы одну цифру";
                if (!hasSpecialChar) return "Пароль должен содержать хотя бы один символ (!? *.)";
                return true;
              },
            })}
          />
          <button
            type="button"
            className="absolute inset-y-0 right-0 pr-3 flex items-center text-gray-400 hover:text-gray-600"
            onClick={() => setShowPassword(!showPassword)}
          >
            {showPassword ? <FaEyeSlash /> : <FaEye />}
          </button>
          {errors.password && (
            <p className="mt-1 text-sm text-red-600">{errors.password.message}</p>
          )}
        </div>
      </div>
          <div>
            <button
              type="submit"
              className="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm/6 font-semibold text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
            >
              Регистрация
            </button>
          </div>
        </form>

        <p className="mt-10 text-center text-sm/6 text-gray-500">
          Уже есть аккаунт?
          <Link
            to="/login"
            className="font-semibold text-indigo-600 hover:text-indigo-500"
          >
            {" "}
            Вход
          </Link>
        </p>
      </div>
    </div>
  );
};

export default RegistrationPage;