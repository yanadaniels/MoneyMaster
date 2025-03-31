import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../Context/AuthContext";

import LoginIcon from "../assets/icons/login.svg?react";
import ProfileIcon from "../assets/icons/profile.svg?react";
import LogoIcon from "../assets/icons/logo.svg";

import authService from "../services/authService";
import DropdownMenu from "./DropdownMenu";

const Header: React.FC = () => {
  const { state, dispatch } = useAuth();
  const { user } = state;
  const navigate = useNavigate();

  const handleLogout = () => {
    authService.logout(dispatch);
    navigate("/login");
  };

  return (
    <header className="bg-indigo-400 text-white py-4 drop-shadow-md relative z-1">
      <div className="container mx-auto w-full max-w-[1700px] flex justify-between items-center">
        <div className="flex items-center">
          <LoginIcon className="w-12 h-12 fill-white rotate-180" />
          <h1 className="text-3x1 font-bold">Money Master</h1>
        </div>
        <nav>
          <ul className="flex items-center space-x-4">
            <li></li>
          </ul>
          {user ? (
            <div className="flex items-center gap-4">
              <span>{user.userName}</span>
              <DropdownMenu
                items={[
                  { label: "Профиль", onClick: () => navigate("/profile") },
                  { label: "Настройки", onClick: () => navigate("/settings") },
                  { label: "Выйти", onClick: handleLogout },
                ]}
              >
                <ProfileIcon className="w-7 h-7 fill-none stroke-0 stroke-current" />
              </DropdownMenu>
            </div>
          ) : (
            <Link
              to="/login"
              className="text-white flex items-center hover:text-gray-800 "
            >
              <span className="mr-1">Войти</span>
              <LoginIcon className="w-5 h-5 fill-none stroke-2 stroke-current" />
            </Link>
          )}
        </nav>
      </div>
    </header>
  );
};

export default Header;
