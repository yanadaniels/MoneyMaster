import { useNavigate } from "react-router-dom";
import authService from "../services/authService";
import { useAuth } from "../Context/AuthContext";
import Transactions from "../components/Transactions";
import Accounts from "../components/Accounts";

const HomePage: React.FC = () => {
  const navigate = useNavigate();
  const { state } = useAuth();
  const user = state.user;

  //   const handleLogout = () => {
  //     authService.logout();
  //     navigate("/login");
  //   };

  return (
    <div className="flex items-beetwen w-full h-screen bg-gray-100">
      <Accounts />
      {/* <Transactions /> */}
    </div>
  );
};

export default HomePage;
