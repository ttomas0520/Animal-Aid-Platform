/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

import {
  AuthResponseDTO,
  CategoryRequestDto,
  CreateRoleDTO,
  LoginDTO,
  RegisterDTO,
  RoleDTO,
  UserDetailDTO,
} from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Api<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags Categories
   * @name CategoriesCreate
   * @request POST:/api/categories
   * @secure
   */
  categoriesCreate = (data: CategoryRequestDto, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/categories`,
      method: "POST",
      body: data,
      secure: true,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Categories
   * @name CategoriesList
   * @request GET:/api/categories
   * @secure
   */
  categoriesList = (params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/categories`,
      method: "GET",
      secure: true,
      ...params,
    });
  /**
   * No description
   *
   * @tags Categories
   * @name CategoriesDelete
   * @request DELETE:/api/categories
   * @secure
   */
  categoriesDelete = (
    query?: {
      name?: string;
      url?: string;
    },
    params: RequestParams = {},
  ) =>
    this.request<void, any>({
      path: `/api/categories`,
      method: "DELETE",
      query: query,
      secure: true,
      ...params,
    });
  /**
   * No description
   *
   * @tags Categories
   * @name CategoriesDetail
   * @request GET:/api/categories/{id}
   * @secure
   */
  categoriesDetail = (id: number, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/categories/${id}`,
      method: "GET",
      secure: true,
      ...params,
    });
  /**
   * No description
   *
   * @tags Categories
   * @name CategoriesUpdate
   * @request PUT:/api/categories/{id}
   * @secure
   */
  categoriesUpdate = (id: number, data: CategoryRequestDto, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/categories/${id}`,
      method: "PUT",
      body: data,
      secure: true,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Roles
   * @name RolesCreate
   * @request POST:/api/Roles
   * @secure
   */
  rolesCreate = (data: CreateRoleDTO, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/Roles`,
      method: "POST",
      body: data,
      secure: true,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Roles
   * @name RolesList
   * @request GET:/api/Roles
   * @secure
   */
  rolesList = (params: RequestParams = {}) =>
    this.request<RoleDTO[], any>({
      path: `/api/Roles`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags User
   * @name UserRegisterCreate
   * @request POST:/api/User/register
   * @secure
   */
  userRegisterCreate = (data: RegisterDTO, params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/api/User/register`,
      method: "POST",
      body: data,
      secure: true,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags User
   * @name UserLoginCreate
   * @request POST:/api/User/login
   * @secure
   */
  userLoginCreate = (data: LoginDTO, params: RequestParams = {}) =>
    this.request<AuthResponseDTO, any>({
      path: `/api/User/login`,
      method: "POST",
      body: data,
      secure: true,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags User
   * @name UserDetailList
   * @request GET:/api/User/detail
   * @secure
   */
  userDetailList = (params: RequestParams = {}) =>
    this.request<UserDetailDTO, any>({
      path: `/api/User/detail`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags User
   * @name UserList
   * @request GET:/api/User
   * @secure
   */
  userList = (params: RequestParams = {}) =>
    this.request<UserDetailDTO[], any>({
      path: `/api/User`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
}
