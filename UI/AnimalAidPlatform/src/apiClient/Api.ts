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
  CategoryDto,
  CategoryRequestDto,
  CreatePostDTO,
  CreateRoleDTO,
  FeedPost,
  FeedPostResponseDTO,
  LoginDTO,
  NotificationSettingsDto,
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
    this.request<CategoryDto[], any>({
      path: `/api/categories`,
      method: "GET",
      secure: true,
      format: "json",
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
    this.request<CategoryDto, any>({
      path: `/api/categories/${id}`,
      method: "GET",
      secure: true,
      format: "json",
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
   * @tags NotificationSettings
   * @name NotificationSettingsList
   * @request GET:/api/NotificationSettings
   * @secure
   */
  notificationSettingsList = (params: RequestParams = {}) =>
    this.request<NotificationSettingsDto, any>({
      path: `/api/NotificationSettings`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags NotificationSettings
   * @name NotificationSettingsUpdate
   * @request PUT:/api/NotificationSettings
   * @secure
   */
  notificationSettingsUpdate = (data: NotificationSettingsDto, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/NotificationSettings`,
      method: "PUT",
      body: data,
      secure: true,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Post
   * @name PostList
   * @request GET:/api/Post
   * @secure
   */
  postList = (params: RequestParams = {}) =>
    this.request<FeedPostResponseDTO[], any>({
      path: `/api/Post`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Post
   * @name PostCreate
   * @request POST:/api/Post
   * @secure
   */
  postCreate = (data: CreatePostDTO, params: RequestParams = {}) =>
    this.request<number, any>({
      path: `/api/Post`,
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
   * @tags Post
   * @name PostDetail
   * @request GET:/api/Post/{id}
   * @secure
   */
  postDetail = (id: number, params: RequestParams = {}) =>
    this.request<FeedPost, any>({
      path: `/api/Post/${id}`,
      method: "GET",
      secure: true,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags Post
   * @name PostUpdate
   * @request PUT:/api/Post/{id}
   * @secure
   */
  postUpdate = (id: number, data: FeedPost, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/Post/${id}`,
      method: "PUT",
      body: data,
      secure: true,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags Post
   * @name PostDelete
   * @request DELETE:/api/Post/{id}
   * @secure
   */
  postDelete = (id: number, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/Post/${id}`,
      method: "DELETE",
      secure: true,
      ...params,
    });
  /**
   * No description
   *
   * @tags Post
   * @name PostUserDetail
   * @request GET:/api/Post/user/{userId}
   * @secure
   */
  postUserDetail = (userId: string, params: RequestParams = {}) =>
    this.request<FeedPostResponseDTO[], any>({
      path: `/api/Post/user/${userId}`,
      method: "GET",
      secure: true,
      format: "json",
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
