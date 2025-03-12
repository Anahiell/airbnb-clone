import { useState } from "react";
import styles from "../styles/Modal.module.css";
import SignInForm from "./SignInForm";
import RegisterForm from "./RegisterForm";

const Modal = ({ isOpen, onClose }) => {
  const [isRegistering, setIsRegistering] = useState(false); // Флаг переключения между входом и регистрацией

  if (!isOpen) return null;

  return (
    <div className={styles.modalOverlay} onClick={onClose}>
      <div className={styles.modalContent} onClick={(e) => e.stopPropagation()}>
        <button className={styles.closeButton} onClick={onClose}>✖</button>
        
        {/* Переключение между формами */}
        {isRegistering ? (
          <RegisterForm onClose={onClose} toggleAuth={() => setIsRegistering(false)} />
        ) : (
          <SignInForm onClose={onClose} toggleAuth={() => setIsRegistering(true)} />
        )}
      </div>
    </div>
  );
};

export default Modal;
