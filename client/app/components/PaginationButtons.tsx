import React from 'react'

interface PaginationProps {
  pageNumber: number
  nextPage: boolean
  handlePageChange: (newPageNumber: number) => void
}

const Pagination: React.FC<PaginationProps> = ({
  pageNumber,
  nextPage,
  handlePageChange,
}) => {
  const buttonClasses =
    'my-4 inline-flex rounded-lg border px-5 py-2.5 text-center text-sm font-medium transition-all duration-500 ease-in-out hover:bg-gray-300 focus:outline-none focus:ring-4 focus:ring-gray-400 dark:border-dracula-purple-900 dark:bg-dracula-purple-900 dark:text-black dark:hover:border-white dark:hover:bg-black dark:hover:text-white light:border-light-theme-color light:bg-light-theme-color light:text-dark-theme-color light:hover:border-dark-theme-color light:hover:bg-dark-theme-color light:hover:text-light-theme-color'

  return (
    <div className='flex flex-col items-center'>
      <div className='xs:mt-0 mt-2 inline-flex space-x-4'>
        {pageNumber > 1 && (
          <button
            onClick={() => handlePageChange(pageNumber - 1)}
            className={buttonClasses}
            aria-label='Ir para página anterior'
          >
            Anterior
          </button>
        )}
        {nextPage && (
          <button
            onClick={() => handlePageChange(pageNumber + 1)}
            className={buttonClasses}
            aria-label='Ir para próxima página'
          >
            Próximo
          </button>
        )}
      </div>
    </div>
  )
}

export default Pagination
