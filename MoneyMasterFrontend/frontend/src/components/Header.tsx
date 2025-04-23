import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../Context/AuthContext";

import LoginIcon from "../assets/icons/login.svg?react";
import ProfileIcon from "../assets/icons/profile.svg?react";

import { authService } from "@/services/authService";
import DropdownMenu from "./DropdownMenu";
import IconRenderer from "./IconRenderer";
import avatarImg from "@/assets/images/Neirokot.jpg";

const Header: React.FC = () => {
  const { state, dispatch } = useAuth();
  const { user } = state;
  const navigate = useNavigate();

  const handleLogout = () => {
    authService.logout(dispatch);
    navigate("/login");
  };

  return (
    <header className="bg-blue-500 text-white py-4 drop-shadow-md relative z-1">
      <div className="flex justify-between items-center px-4">
        <div className="flex items-center">
          <IconRenderer
            iconName="faPiggyBank"
            className="text-3xl text-white mr-3"
          />
          <h1 className="text-3x1 font-bold">Money Master</h1>
        </div>
        <nav>
          <ul className="flex items-center space-x-4">
            <li></li>
          </ul>
          {user ? (
            <div className="flex items-center gap-2">
              <span>{user.userName}</span>
              <DropdownMenu
                items={[
                  { label: "Профиль", onClick: () => navigate("/profile") },
                  { label: "Настройки", onClick: () => navigate("/settings") },
                  { label: "Выйти", onClick: handleLogout },
                ]}
              >
                {/* <ProfileIcon className="w-7 h-7 fill-none stroke-0 stroke-current" /> */}
                <img
                  src={avatarImg}
                  className="w-9 h-9 fill-none stroke-2 stroke-current radius rounded-full"
                />
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
