import { useState, useEffect } from 'react'
import { getPosts } from '../utils/api'
import { Post } from '../models'
import { AxiosError } from 'axios'

export const usePosts = (initialPage: number, initialPageSize: number) => {
  const [isLoading, setIsLoading] = useState(true)
  const [data, setData] = useState<Post[]>([])
  const [pageNumber, setPageNumber] = useState(initialPage)
  const [pageSize] = useState(initialPageSize)
  const [totalCount] = useState(0)
  const [nextPage, setNextPage] = useState(false)
  const [error, setError] = useState<AxiosError | null>(null)

  const fetchData = async (pageNumber: number, pageSize: number) => {
    try {
      const result = await getPosts(pageNumber, pageSize)
      return result.data
    } catch (error) {
      setError(error as AxiosError)
    }
  }

  const handlePageChange = async (newPageNumber: number) => {
    setIsLoading(true)
    const fetchedData = await fetchData(newPageNumber, pageSize)
    setData(fetchedData.items)
    setPageNumber(fetchedData.currentPage)
    setNextPage(fetchedData.hasNextPage)
    setIsLoading(false)
  }

  useEffect(() => {
    handlePageChange(pageNumber)
  }, [pageNumber, pageSize])

  return {
    isLoading,
    data,
    pageNumber,
    pageSize,
    totalCount,
    nextPage,
    handlePageChange,
    error,
  }
}
