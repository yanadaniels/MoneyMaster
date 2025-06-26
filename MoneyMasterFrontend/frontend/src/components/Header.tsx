import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../Context/AuthContext";
import LoginIcon from "../assets/icons/login.svg?react";
import ProfileIcon from "../assets/icons/profile.svg?react";
import { authService } from "@/services/authService";
import DropdownMenu from "./DropdownMenu";
import IconRenderer from "./IconRenderer";
import avatarImg from "@/assets/images/Neirokot.jpg";
import { useState } from "react";
import ProfileModal from "./user/ProfileModal";

const Header: React.FC = () => {
  const { state, dispatch } = useAuth();
  const { user, isLoading } = state;
  const [isProfileModalOpen, setIsProfileModalOpen] = useState(false);
  const navigate = useNavigate();

  const handleLogout = () => {
    authService.logout(dispatch);
    navigate("/login");
  };

  const HeaderSkeleton = () => (
    <header className="bg-blue-500 text-white py-4 drop-shadow-md relative z-1">
      <div className="flex justify-between items-center px-4">
        <div className="flex items-center">
          <div className="animate-pulse bg-blue-400 rounded-full w-8 h-8 mr-3"></div>
          <div className="animate-pulse bg-blue-400 rounded h-6 w-32"></div>
        </div>
        <div className="animate-pulse bg-blue-400 rounded-full w-9 h-9"></div>
      </div>
    </header>
  );

  const handleUpdateUser = () => {};

  if (isLoading) {
    return <HeaderSkeleton />;
  }

  return (
    <>
    <header className="bg-blue-500 text-white py-4 drop-shadow-md">
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
                  { label: "Профиль", onClick: () => setIsProfileModalOpen(true) },
                  { label: "Выйти", onClick: handleLogout },
                ]}
              >
                <img
                  src={avatarImg}
                  className="w-9 h-9 fill-none stroke-2 stroke-current radius rounded-full"
                  alt="User avatar"
                />
              </DropdownMenu>
            </div>
          ) : (
            <Link
              to="/login"
              className="text-white flex items-center hover:text-gray-800 transition-colors duration-200"
            >
              <span className="mr-1">Войти</span>
              <LoginIcon className="w-5 h-5 fill-none stroke-2 stroke-current" />
            </Link>
          )}
        </nav>
      </div>
    </header>
    
    <ProfileModal
      isOpen={isProfileModalOpen}
      onClose={() => setIsProfileModalOpen(false)}
      onConfirm={handleUpdateUser}
      user={user}
    />
    </>
  );
};

export default Header;