import styles from "./Modal.module.css";
import PropTypes from "prop-types";

const Modal = ({ isOpen, onClose, children }) => {
  if (!isOpen) return null;

  return (
    <div className={styles.modalOverlay} onClick={onClose}>
      <div className={styles.modalContent} onClick={(e) => e.stopPropagation()}>
        <button className={styles.closeButton} onClick={onClose}>✖</button>
        {children}
      </div>
    </div>
  );
};

// Валидация пропсов
Modal.propTypes = {
  isOpen: PropTypes.bool.isRequired,       // isOpen должно быть булевым значением
  onClose: PropTypes.func.isRequired,      // onClose должно быть функцией
  children: PropTypes.node.isRequired,     // children могут быть любым допустимым React-элементом
};
export default Modal;
