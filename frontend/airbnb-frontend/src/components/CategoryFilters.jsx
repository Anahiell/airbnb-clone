import styles from "../styles/CategoryFilters.module.css";

const categories = [
  { id: "all", label: "Все" },
  { id: "house", label: "Дома" },
  { id: "apartment", label: "Апартаменты" },
  { id: "villa", label: "Виллы" },
  { id: "cabin", label: "Хижины" },
];

const CategoryFilters = ({ selectedCategory, onSelectCategory }) => {
  return (
    <div className={styles.filters}>
      {categories.map(({ id, label }) => (
        <button
          key={id}
          className={`${styles.filter} ${selectedCategory === id ? styles.active : ""}`}
          onClick={() => onSelectCategory(id)}
        >
          {label}
        </button>
      ))}
    </div>
  );
};

export default CategoryFilters;
