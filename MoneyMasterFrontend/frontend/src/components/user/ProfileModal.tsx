import React, { useState } from "react";
import Modal from "../Modal";
import {
  AccountResponse,
  CategoryResponse,
  CreateTransactionRequest,
} from "@/types";
import { User } from "@/types/user";
import avatarImg from "@/assets/images/Neirokot.jpg";

interface ProfileModalProps {
  isOpen: boolean;
  onClose: () => void;
  onConfirm: (data: CreateTransactionRequest) => void;
  user: User | null
}

const ProfileModal: React.FC<ProfileModalProps> = ({
  isOpen,
  onClose,
  onConfirm,
  user
}) => {
  const [amount, setAmount] = useState<number | string>("");
  const [selectedCategory, setSelectedCategory] =
    useState<CategoryResponse | null>(null);
  const [description, setDescription] = useState<string>("");
  const [addCategory, setAddCategory] = useState(false);

  const handleSubmit = () => {

  };

  return (
    <Modal
      isOpen={isOpen}
      onClose={onClose}
      title={`Профиль`}
    >
      <div className="mb-4 space-y-4">
        <div
          className="flex justify-center">
          <img
            src={avatarImg}
            className="w-25 h-25 fill-none stroke-2 stroke-current radius rounded-full"
            alt="User avatar"
          />
        </div>
        <div className="flex flex-col justify-center items-center">
          <span className="text-gray-400">Имя</span>
          {user?.userName}
        </div>
        <div className="flex flex-col justify-center items-center">
          <span className="text-gray-400">Email</span>
          {user?.email}
        </div>
      </div>

      {/* <div className="flex justify-end gap-2 mt-8">
        <button
          onClick={onClose}
          className="bg-gray-500 text-white px-4 py-2 rounded-lg hover:bg-gray-600"
        >
          Отменить
        </button>
        <button
          onClick={handleSubmit}
          className="bg-green-500 text-white px-4 py-2 rounded-lg hover:bg-green-600"
        >
          ОК
        </button>
      </div> */}
    </Modal>
  );
};

export default ProfileModal;
