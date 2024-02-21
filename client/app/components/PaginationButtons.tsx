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
  return (
    <div className='flex flex-col items-center'>
      <div className='xs:mt-0 mt-2 inline-flex space-x-4'>
        {pageNumber > 1 && (
          <button
            onClick={() => handlePageChange(pageNumber - 1)}
            className={
              'my-4 inline-flex rounded-lg bg-dracula-pink px-5 py-2.5 text-center text-sm font-medium text-white transition-all duration-500 ease-in-out hover:bg-dracula-pink-800 focus:outline-none focus:ring-4 focus:ring-dracula-pink dark:focus:ring-gray-400'
            }
          >
            Anterior
          </button>
        )}
        {nextPage && (
          <button
            onClick={() => handlePageChange(pageNumber + 1)}
            className={
              'my-4 inline-flex rounded-lg bg-dracula-pink px-5 py-2.5 text-center text-sm font-medium text-white transition-all duration-500 ease-in-out hover:bg-dracula-pink-800 focus:outline-none focus:ring-4 focus:ring-dracula-pink dark:focus:ring-gray-400'
            }
          >
            Próximo
          </button>
        )}
      </div>
    </div>
  )
}

export default Pagination
