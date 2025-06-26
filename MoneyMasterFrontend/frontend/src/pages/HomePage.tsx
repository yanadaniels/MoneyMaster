import { useNavigate } from "react-router-dom";
import { useAuth } from "../Context/AuthContext";
import Accounts from "../components/accounts/Accounts";

const HomePage: React.FC = () => {
  const navigate = useNavigate();
  const { state } = useAuth();
  const user = state.user;

  //   const handleLogout = () => {
  //     authService.logout();
  //     navigate("/login");
  //   };
  return (
    <div className="flex items-beetwen w-full bg-gray-100">
      <Accounts />
    </div>
  );
};

export default HomePage;
