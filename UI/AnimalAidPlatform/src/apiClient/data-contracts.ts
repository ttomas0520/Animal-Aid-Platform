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

export interface AuthResponseDTO {
  token?: string | null;
  isSucces?: boolean;
  message?: string | null;
}

export interface CategoryRequestDto {
  name?: string | null;
  urlhandle?: string | null;
}

export interface CreateRoleDTO {
  /** @minLength 1 */
  roleName: string;
}

export interface LoginDTO {
  /** @minLength 1 */
  email: string;
  /** @minLength 1 */
  password: string;
}

export interface RegisterDTO {
  /** @minLength 1 */
  name: string;
  /**
   * @format email
   * @minLength 1
   */
  email: string;
  /** @minLength 1 */
  password: string;
  /** @minLength 1 */
  phoneNumber: string;
  roles?: string[] | null;
}

export interface RoleDTO {
  id?: string | null;
  roleName?: string | null;
  /** @format int32 */
  userCount?: number;
}

export interface UserDetailDTO {
  id?: string | null;
  name?: string | null;
  email?: string | null;
  roles?: string[] | null;
  phoneNumber?: string | null;
  twoFactorEnabled?: boolean;
  phoneNumberConfirmed?: boolean;
  /** @format int32 */
  accessFailedCount?: number;
}
